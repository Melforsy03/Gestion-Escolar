using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomRestriction;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorSubject;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ProfessorSubjectService : IProfessorSubjectService
    {
        private readonly IProfessorSubjectRepository _professorSubjectRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        private readonly Context _context;

        public ProfessorSubjectService(Context context, IProfessorSubjectRepository professorSubjectRepository, ISubjectRepository subjectRepository, IProfessorRepository professorRepository, IMapper mapper)
        {
            _professorSubjectRepository = professorSubjectRepository;
            _professorRepository = professorRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProfessorSubjectResponseDto> CreateProfessorSubjectAsync(ProfessorSubjectDto professorSubjectDto)
        {
            // Mapea el DTO del profesor-asignatura a la entidad de dominio ProfessorSubject
            var professorSubject = _mapper.Map<ProfessorSubject>(professorSubjectDto);

            // Verifica si el profesor y la asignatura existen en la base de datos
            if (_context.Professors.Find(professorSubject.IdProf) == null || _context.Subjects.Find(professorSubject.IdSub) == null)
                return null; // Retorna null si no se encuentra el profesor o la asignatura

            // Obtiene el objeto Profesor y Asignatura correspondientes
            professorSubject.Professor = _professorRepository.GetById(professorSubjectDto.IdProf);
            professorSubject.Subject = _subjectRepository.GetById(professorSubjectDto.IdSub);

            // Crea la relación entre el profesor y la asignatura en la base de datos
            var savedProfessorSubject = await _professorSubjectRepository.CreateAsync(professorSubject);

            // Mapea la entidad guardada de vuelta a un DTO y lo retorna
            return _mapper.Map<ProfessorSubjectResponseDto>(savedProfessorSubject);
        }

        public async Task<ProfessorSubjectResponseDto> DeleteProfessorSubjectByIdAsync(int id)
        {
            // Obtiene la relación profesor-asignatura por su ID
            var professorSubject = _professorSubjectRepository.GetById(id);

            // Mapea la relación obtenida a un DTO para la respuesta
            var professorSubjectDto = _mapper.Map<ProfessorSubjectResponseDto>(professorSubject);

            // Elimina la relación de profesor-asignatura por su ID en la base de datos
            await _professorSubjectRepository.DeleteByIdAsync(id);

            // Retorna el DTO de la relación eliminada
            return professorSubjectDto;
        }

        public async Task<IEnumerable<ProfessorSubjectResponseDto>> ListProfessorSubjectAsync()
        {
            // Obtiene todas las relaciones entre profesores y asignaturas desde el repositorio
            var professorSubjects = await _professorSubjectRepository.ListAsync();

            var list = professorSubjects.ToList(); // Convierte a lista para su manipulación
            List<ProfessorSubjectResponseDto> professorSubjects_List = new(); // Inicializa una lista para almacenar los DTOs

            // Itera sobre cada relación y mapea a DTOs
            for (int i = 0; i < professorSubjects.Count(); i++)
            {
                professorSubjects_List.Add(_mapper.Map<ProfessorSubjectResponseDto>(list[i])); // Agrega el DTO a la lista
            }

            // Retorna la lista de DTOs de relaciones entre profesores y asignaturas
            return professorSubjects_List;
        }


    }

}
