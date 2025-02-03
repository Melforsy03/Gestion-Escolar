using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Professor;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Student;
using SchoolManagement.Application.Common;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly Context _context;
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public StudentService(Context context, IStudentRepository studentRepository, IMapper mapper, Triggers trigger, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _context = context;
            _mapper = mapper;
            _trigger = trigger;
        }

        public async Task<StudentCreateResponseDto> CreateStudentAsync(StudentDto studentDto)
        {
            // Mapea el DTO de estudiante a la entidad de dominio Student
            var student = _mapper.Map<Student>(studentDto);

            // Registra al usuario asociado al estudiante y obtiene su información
            (User, string) User = await _trigger.RegisterUser(studentDto.NameStud, "Student");

            // Asigna el ID del usuario al objeto estudiante
            student.UserId = User.Item1.Id;

            // Asigna el curso por defecto (ID 1) al estudiante
            student.Course = _courseRepository.GetById(1);

            // Crea el nuevo estudiante en la base de datos
            var savedStudent = await _studentRepository.CreateAsync(student);

            // Mapea el estudiante guardado de vuelta a un DTO
            studentDto = _mapper.Map<StudentDto>(savedStudent);

            // Crea un DTO de respuesta para el nuevo estudiante creado
            StudentCreateResponseDto answer = new StudentCreateResponseDto();
            answer.Id = student.IdStud; // Asigna el ID del estudiante a la respuesta
            answer.IdC = student.IdC; // Asigna el ID del curso al que pertenece el estudiante
            answer.student = studentDto; // Asigna el DTO del estudiante a la respuesta
            answer.UserName = User.Item1.UserName; // Asigna el nombre de usuario a la respuesta
            answer.Password = User.Item2; // Asigna la contraseña a la respuesta

            return answer; // Retorna el DTO con los detalles del nuevo estudiante creado
        }

        public async Task<IEnumerable<StudentInfoBad>> GetBadStudents()
        {
            // Obtiene todos los estudiantes desde el contexto
            var students = _context.Students.ToList();

            List<StudentInfoBad> badStudents = new List<StudentInfoBad>(); // Inicializa una lista para almacenar estudiantes con malas evaluaciones

            // Itera sobre cada estudiante para evaluar su rendimiento académico
            foreach (var student in students)
            {
                // Obtiene las asignaturas del estudiante
                var studentSubjects = _context.StudentSubjects.Where(ss => ss.IdStud == student.IdStud).ToList();
                List<Subject> subjects = new List<Subject>();

                // Itera sobre las asignaturas del estudiante para calcular las evaluaciones
                foreach (var ss in studentSubjects)
                {
                    var gradesTemp = _context.ProfessorStudentSubjects.Where(pss => pss.IdStudSub == ss.IdStudSub).Select(k => k.StudentGrades).ToList();
                    float average = _trigger.GetAverage(gradesTemp); // Calcula la evaluación promedio

                    if (average < 50 && average >= 0) // Si la evaluación es menor a 50, se considera mala
                    {
                        subjects.Add(_context.Subjects.Where(s => s.IdSub == ss.IdSub).First()); // Agrega la asignatura a la lista de malas asignaturas
                    }
                }

                if (subjects.Count() >= 2) // Si el estudiante tiene 2 o más malas asignaturas
                {
                    List<Professor> checkedProfessors = new List<Professor>(); // Inicializa una lista para almacenar profesores que han evaluado al estudiante

                    foreach (var s in subjects)
                    {
                        List<Professor> professors = new List<Professor>();
                        var professorSubjects = _context.ProfessorSubjects.Where(ps => ps.IdSub == s.IdSub).ToList(); // Obtiene los profesores que imparten esa asignatura

                        foreach (var ps in professorSubjects)
                        {
                            professors.Add(_context.Professors.Where(p => p.IdProf == ps.IdProf).First()); // Agrega los profesores a la lista
                        }

                        foreach (var p in professors)
                        {
                            var grades = _context.ProfessorStudentSubjects.Where(pss => pss.IdProf == p.IdProf && pss.IdStudSub == _context.StudentSubjects.Where(ss => ss.IdSub == s.IdSub && ss.IdStud == student.IdStud).First().IdStudSub).Select(g => g.StudentGrades).ToList();

                            foreach (var g in grades)
                            {
                                if (g < 50) // Si alguna evaluación es menor a 50, se agrega el profesor a la lista de verificación
                                {
                                    checkedProfessors.Add(p);
                                }
                            }
                        }
                    }

                    List<int> evals = new List<int>(); // Inicializa una lista para almacenar las evaluaciones de los profesores verificados

                    foreach (var p in checkedProfessors)
                    {
                        evals.AddRange(_context.ProfStudSubCourses.Where(pssc => pssc.IdProf == p.IdProf && pssc.IdCourse == student.IdC).Select(g => g.Evaluation).ToList());
                    }

                    StudentInfoBad badStudent = new StudentInfoBad(); // Crea un objeto para almacenar información sobre el mal estudiante
                    badStudent.StudentId = student.IdStud; // Asigna el ID del estudiante
                    badStudent.StudentName = student.NameStud; // Asigna el nombre del estudiante
                    badStudent.Curso = _context.Courses.Where(c => c.IdC == student.IdC).First().CourseName; // Asigna el nombre del curso al que pertenece el estudiante
                    badStudent.ProfessorsAvarageEvaluation = _trigger.GetAverage(evals); // Calcula y asigna la evaluación promedio de los profesores

                    badStudents.Add(badStudent); // Agrega al listado de estudiantes malos
                }
            }

            return badStudents; // Retorna la lista de estudiantes con malas evaluaciones
        }

        public async Task<StudentResponseDto> DeleteStudentByIdAsync(int studentId)
        {
            // Obtiene el estudiante por su ID desde el repositorio
            var student = _studentRepository.GetById(studentId);

            if (student.IsDeleted)
            {
                return null; // Retorna null si ya está eliminado
            }

            student.IsDeleted = true; // Marca al estudiante como eliminado

            await _studentRepository.UpdateAsync(student); // Actualiza el estado en la base de datos

            StudentResponseDto answer = new StudentResponseDto();
            answer.Id = student.IdStud; // Asigna el ID del estudiante a la respuesta
            answer.Idc = student.IdC;   // Asigna el ID del curso al que pertenece el estudiante 
            answer.student = _mapper.Map<StudentDto>(student); // Mapea y asigna el DTO del estudiante

            return answer; // Retorna el DTO con los detalles del estudiante eliminado
        }

        public async Task<IEnumerable<StudentResponseDto>> ListStudentAsync()
        {
            // Obtiene todos los estudiantes desde el repositorio
            var students = await _studentRepository.ListAsync();

            var list = students.ToList();
            List<StudentResponseDto> Students_List = new();  // Inicializa una lista para almacenar los DTOs

            for (int i = 0; i < students.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    StudentResponseDto answer = new StudentResponseDto();
                    answer.Id = list[i].IdStud;  // Asigna el ID del estudiante a la respuesta 
                    answer.Idc = list[i].IdC;   // Asigna el ID del curso al que pertenece el estudiante 
                    answer.student = _mapper.Map<StudentDto>(list[i]);  // Mapea y asigna el DTO del estudiante

                    Students_List.Add(answer);  // Agrega a la lista de resultados 
                }
            }

            return Students_List;  // Retorna la lista de DTOs de estudiantes no eliminados 
        }

        public async Task<StudentResponseDto> UpdateStudentAsync(StudentResponseDto studentInfo)
        {
            var student = _studentRepository.GetById(studentInfo.Id);  // Obtiene al estudiante por su ID desde el DTO

            if (student.IsDeleted)
            {
                return null;  // Retorna null si está eliminado 
            }

            _mapper.Map(studentInfo.student, student);  // Mapea los cambios desde el DTO a la entidad existente 

            var course = _courseRepository.GetById(studentInfo.Idc);  // Obtiene el curso correspondiente al ID proporcionado en el DTO 

            if (course == null) return null;  // Retorna null si no se encuentra el curso 

            student.Course = course;  // Asigna el curso al objeto estudiante 

            await _studentRepository.UpdateAsync(student);  // Actualiza la entidad en la base de datos 

            StudentResponseDto answer = new StudentResponseDto();
            answer.Id = student.IdStud;   // Asigna el ID del estudiante a la respuesta 
            answer.Idc = student.IdC;      // Asigna el ID del curso al que pertenece 
            answer.student = _mapper.Map<StudentDto>(student);  // Mapea y asigna el DTO actualizado 

            return answer;  // Retorna la respuesta con los detalles actualizados del estudiante.
        }


    }
}
