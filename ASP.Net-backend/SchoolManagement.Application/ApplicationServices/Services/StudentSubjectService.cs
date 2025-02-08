using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.StudentSubject;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class StudentSubjectService : IStudentSubjectService
    {
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public StudentSubjectService(IStudentSubjectRepository studentSubjectRepository,  IMapper mapper, IStudentRepository studentRepository, ISubjectRepository subjectRepository)
        {
            _studentSubjectRepository = studentSubjectRepository;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<StudentSubjectResponseDto> CreateStudentSubjectAsync(StudentSubjectDto studentSubjectDto)
        {
            // Mapea el DTO de la relación estudiante-asignatura a la entidad de dominio StudentSubject
            var studentSubject = _mapper.Map<StudentSubject>(studentSubjectDto);

            // Obtiene la asignatura correspondiente utilizando el repositorio
            studentSubject.Subject = _subjectRepository.GetById(studentSubjectDto.IdSub);

            // Obtiene el estudiante correspondiente utilizando el repositorio
            studentSubject.Student = _studentRepository.GetById(studentSubjectDto.IdStud);

            // Crea la relación en la base de datos y guarda el resultado
            var savedStudentSubject = await _studentSubjectRepository.CreateAsync(studentSubject);

            // Mapea la entidad guardada de vuelta a un DTO y lo retorna
            return _mapper.Map<StudentSubjectResponseDto>(savedStudentSubject);
        }

        public async Task<IEnumerable<StudentSubjectResponseDto>> ListStudentSubjectAsync()
        {
            // Obtiene todas las relaciones entre estudiantes y asignaturas desde el repositorio
            var studentsSubject = await _studentSubjectRepository.ListAsync();

            var list = studentsSubject.ToList(); // Convierte a lista para su manipulación
            List<StudentSubjectResponseDto> studentsSubject_List = new(); // Inicializa una lista para almacenar los DTOs

            // Itera sobre cada relación y mapea a DTOs
            for (int i = 0; i < studentsSubject.Count(); i++)
            {
                studentsSubject_List.Add(_mapper.Map<StudentSubjectResponseDto>(list[i])); // Agrega el DTO a la lista
            }

            return studentsSubject_List; // Retorna la lista de DTOs de relaciones entre estudiantes y asignaturas
        }

        public async Task<StudentSubjectResponseDto> UpdateStudentSubjectAsync(StudentSubjectResponseDto studentSubjectDto)
        {
            // Obtiene la relación estudiante-asignatura por su ID desde el DTO
            var studentSubject = _studentSubjectRepository.GetById(studentSubjectDto.IdStudSub);

            // Mapea los cambios desde el DTO a la entidad existente
            _mapper.Map(studentSubjectDto, studentSubject);

            // Actualiza la entidad en la base de datos
            await _studentSubjectRepository.UpdateAsync(studentSubject);

            // Mapea y retorna la entidad actualizada como DTO
            return _mapper.Map<StudentSubjectResponseDto>(studentSubject);
        }

    }
}
