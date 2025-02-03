using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorStudentSubject;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Domain.Role;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ProfessorStudentSubjectService : IProfessorStudentSubjectService
    {
        private readonly IProfessorStudentSubjectRepository _professorStudentSubjectRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly Context _context;
        private readonly IMapper _mapper;

        public ProfessorStudentSubjectService(Context context, ISubjectRepository subjectRepository, IProfessorStudentSubjectRepository professorStudentSubjectRepository, IProfessorRepository professorRepository, IStudentSubjectRepository studentSubjectRepository,IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _context = context;
            _professorStudentSubjectRepository = professorStudentSubjectRepository;
            _professorRepository = professorRepository;
            _studentSubjectRepository = studentSubjectRepository;
            _mapper = mapper;
        }

        public async Task<ProfessorStudentSubjectResponseDto> CreateProfessorStudentSubjectAsync(ProfessorStudentSubjectDto professorStudentSubjectDto)
        {
            // Obtiene el usuario basado en el nombre de usuario proporcionado en el DTO
            var user = _context.Users.Where(u => u.UserName == professorStudentSubjectDto.UserName).FirstOrDefault();

            // Obtiene el rol del usuario
            string role = _context.Roles.Where(r => r.Id == _context.UserRoles.Where(ur => ur.UserId == user.Id).FirstOrDefault().RoleId).FirstOrDefault().Name;

            // Verifica si el rol es Profesor o SuperAdmin
            if (role == Role.Professor || role == Role.SuperAdmin)
            {
                // Obtiene el profesor correspondiente al usuario
                var professor = _context.Professors.Where(p => p.UserId == user.Id).First();

                // Obtiene la asignatura del estudiante correspondiente
                var studentSubject = _context.StudentSubjects.Where(ss => ss.IdStud == professorStudentSubjectDto.IdStud && ss.IdStud == professorStudentSubjectDto.IdStud).First();

                // Crea una nueva relación entre profesor y asignatura del estudiante
                var profStudSub = new ProfessorStudentSubject
                {
                    IdProf = professor.IdProf,
                    IdStudSub = studentSubject.IdStudSub,
                    Professor = professor,
                    StudentSubject = studentSubject,
                    StudentGrades = professorStudentSubjectDto.StudentGrades
                };

                // Agrega la relación a la base de datos
                _context.ProfessorStudentSubjects.Add(profStudSub);
                _context.SaveChanges();

                // Retorna un DTO con los detalles de la relación creada
                return new ProfessorStudentSubjectResponseDto
                {
                    IdProfStudSub = profStudSub.IdProfStudSub,
                    IdStud = studentSubject.IdStud,
                    IdSub = studentSubject.IdSub,
                    studentName = _context.Students.Where(s => s.IdStud == studentSubject.IdStud).First().NameStud,
                    subjectName = _context.Subjects.Where(s => s.IdSub == studentSubject.IdSub).First().NameSub,
                    professorName = professor.NameProf,
                    StudentGrades = professorStudentSubjectDto.StudentGrades
                };
            }
            else if (role == Role.Secretary) // Si el rol es Secretario
            {
                // Obtiene el profesor correspondiente a la asignatura especificada
                var professor = _context.Professors.Where(p => p.IdProf == _context.ProfessorSubjects.Where(ps => ps.IdSub == professorStudentSubjectDto.IdSub).First().IdProf).FirstOrDefault();

                // Obtiene la asignatura del estudiante correspondiente
                var studentSubject = _context.StudentSubjects.Where(ss => ss.IdStud == professorStudentSubjectDto.IdStud && ss.IdStud == professorStudentSubjectDto.IdStud).First();

                // Crea una nueva relación entre profesor y asignatura del estudiante
                var profStudSub = new ProfessorStudentSubject
                {
                    IdProf = professor.IdProf,
                    IdStudSub = studentSubject.IdStudSub,
                    Professor = professor,
                    StudentSubject = studentSubject,
                    StudentGrades = professorStudentSubjectDto.StudentGrades
                };

                // Agrega la relación a la base de datos
                _context.ProfessorStudentSubjects.Add(profStudSub);
                _context.SaveChanges();

                // Obtiene información del secretario que está realizando la acción
                var secretary = _context.Secretaries.Where(s => s.UserId == user.Id).First();

                // Retorna un DTO con los detalles de la relación creada y el nombre del secretario
                return new ProfessorStudentSubjectResponseDto
                {
                    IdProfStudSub = profStudSub.IdProfStudSub,
                    IdStud = studentSubject.IdStud,
                    IdSub = studentSubject.IdSub,
                    studentName = _context.Students.Where(s => s.IdStud == studentSubject.IdStud).First().NameStud,
                    subjectName = _context.Subjects.Where(s => s.IdSub == studentSubject.IdSub).First().NameSub,
                    professorName = secretary.NameS, // Nombre del secretario en lugar del profesor
                    StudentGrades = professorStudentSubjectDto.StudentGrades
                };
            }

            throw new NotImplementedException(); // Lanza una excepción si no se cumple ninguna de las condiciones anteriores
        }

        public async Task<PSSResponseGetStudents> GetStudentsForSubjectAsync(int subjectId)
        {
            // Obtiene la asignatura por su ID desde el repositorio de asignaturas
            var subject = _subjectRepository.GetById(subjectId);

            // Obtiene los estudiantes que están inscritos en la asignatura especificada
            var students = _context.Students.Where(st => st.Subjects.Contains(subject)).ToList();

            if (students == null) return null; // Retorna null si no hay estudiantes

            return new PSSResponseGetStudents { students = students }; // Retorna un objeto con la lista de estudiantes
        }

        public async Task<PSSResponseGetSubjects> GetSubjectsOfProfessorAsync(string UserName)
        {
            // Obtiene el profesor correspondiente al nombre de usuario proporcionado
            var professor = _context.Professors.Where(p => p.UserId == _context.Users.Where(u => u.UserName == UserName).FirstOrDefault().Id).FirstOrDefault();

            if (professor == null) return null; // Retorna null si no se encuentra al profesor

            // Obtiene las asignaturas que imparte el profesor
            var subjects = _context.Subjects.Where(s => s.Professors.Contains(professor)).ToList();

            return new PSSResponseGetSubjects { subjects = subjects }; // Retorna un objeto con la lista de asignaturas del profesor
        }

        public Task<PSSResponseGetSubjects> GetSubjectsOfProfessorAsync(ProfessorStudentSubjectConsultDto professorStudentSubjectConsultDto)
        {
            throw new NotImplementedException(); // Lanza una excepción ya que este método no está implementado.
        }

        public async Task<IEnumerable<ProfessorStudentSubjectResponseDto>> ListProfessorStudentSubjectAsync()
        {
            // Obtiene todas las relaciones entre profesores y asignaturas de estudiantes desde el repositorio
            var list = _context.ProfessorStudentSubjects.ToList();

            List<ProfessorStudentSubjectResponseDto> professorStudentSubjects_List = new List<ProfessorStudentSubjectResponseDto>();

            // Itera sobre cada relación y crea un DTO para cada una de ellas
            for (int i = 0; i < list.Count(); i++)
            {
                var studentSubject = _context.StudentSubjects.Where(ss => ss.IdStudSub == list[i].IdStudSub).First();

                professorStudentSubjects_List.Add(new ProfessorStudentSubjectResponseDto
                {
                    studentName = _context.Students.Where(s => s.IdStud == studentSubject.IdStud).First().NameStud,  // Nombre del estudiante asociado
                    subjectName = _context.Subjects.Where(s => s.IdSub == studentSubject.IdSub).First().NameSub,  // Nombre de la asignatura asociada
                    professorName = _context.Professors.Where(p => p.IdProf == list[i].IdProf).First().NameProf,  // Nombre del profesor asociado
                    IdProfStudSub = list[i].IdProfStudSub,  // ID de la relación entre profesor y asignatura del estudiante 
                    IdStud = studentSubject.IdStud,  // ID del estudiante 
                    IdSub = studentSubject.IdSub,  // ID de la asignatura 
                    StudentGrades = list[i].StudentGrades  // Calificaciones del estudiante en esa asignatura 
                });
            }

            return professorStudentSubjects_List;  // Retorna la lista de relaciones entre profesores y asignaturas de estudiantes como DTOs.
        }

        public async Task<bool> UpdateProfessorStudentSubjectAsync(int profStudSubID, float studentGrade)
        {
            // Busca la relación entre profesor y asignatura del estudiante por su ID 
            var profStudSub = await _context.ProfessorStudentSubjects.FindAsync(profStudSubID);

            profStudSub.StudentGrades = studentGrade;  // Actualiza las calificaciones del estudiante

            await _context.SaveChangesAsync();  // Guarda los cambios en el contexto

            return true;  // Retorna verdadero indicando que la operación fue exitosa.
        }

        public async Task<IEnumerable<ProfessorStudentSubjectResponseDto>> ListProfessorStudentSubjectByUserNameAsync(string UserName)
        {
            // Obtiene el profesor correspondiente al nombre de usuario proporcionado 
            var professor = _context.Professors.Where(p => p.UserId == _context.Users.Where(u => u.UserName == UserName).First().Id).First();

            // Obtiene las relaciones entre el profesor y las asignaturas de los estudiantes 
            var professorStudentSubjects = _context.ProfessorStudentSubjects.Where(pss => pss.IdProf == professor.IdProf);

            var list = professorStudentSubjects.ToList();
            List<ProfessorStudentSubjectResponseDto> professorStudentSubjects_List = new();

            for (int i = 0; i < list.Count(); i++)
            {
                var studentSubject = _context.StudentSubjects.Where(ss => ss.IdStudSub == list[i].IdStudSub).First();

                professorStudentSubjects_List.Add(new ProfessorStudentSubjectResponseDto
                {
                    studentName = _context.Students.Where(s => s.IdStud == studentSubject.IdStud).First().NameStud,  // Nombre del estudiante asociado 
                    subjectName = _context.Subjects.Where(s => s.IdSub == studentSubject.IdSub).First().NameSub,  // Nombre de la asignatura asociada 
                    professorName = professor.NameProf,  // Nombre del profesor asociado 
                    IdProfStudSub = list[i].IdProfStudSub,  // ID de la relación entre profesor y asignatura del estudiante 
                    IdStud = studentSubject.IdStud,  // ID del estudiante 
                    IdSub = studentSubject.IdSub,  // ID de la asignatura 
                    StudentGrades = list[i].StudentGrades  // Calificaciones del estudiante en esa asignatura 
                });
            }

            return professorStudentSubjects_List;  // Retorna la lista de relaciones entre profesores y asignaturas de estudiantes como DTOs.
        }

    }
}
