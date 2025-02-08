using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Subject;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassRoomRepository _classRoomRepository;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepository subjectRepository,  IMapper mapper, IClassRoomRepository classRoomRepository)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _classRoomRepository = classRoomRepository;
        }

        public async Task<SubjectResponseDto> CreateSubjectAsync(SubjectDto subjectDto)
        {
            // Mapea el DTO de asignatura a la entidad de dominio Subject
            var subject = _mapper.Map<Domain.Entities.Subject>(subjectDto);

            // Obtiene el aula correspondiente utilizando el repositorio
            subject.classRoom = await _classRoomRepository.GetByIdAsync(subject.IdClassRoom);

            // Crea la nueva asignatura en la base de datos
            var savedSubject = await _subjectRepository.CreateAsync(subject);

            // Mapea la entidad guardada de vuelta a un DTO y lo retorna
            return _mapper.Map<SubjectResponseDto>(savedSubject);
        }

        public async Task<SubjectResponseDto> DeleteSubjectByIdAsync(int subjectId)
        {
            // Obtiene la asignatura por su ID
            var subject = _subjectRepository.GetById(subjectId);

            // Verifica si la asignatura ya está marcada como eliminada
            if (subject.IsDeleted) return null;

            // Marca la asignatura como eliminada
            subject.IsDeleted = true;

            // Actualiza el estado en la base de datos
            await _subjectRepository.UpdateAsync(subject);

            // Mapea y retorna el DTO de la asignatura eliminada
            return _mapper.Map<SubjectResponseDto>(subject);
        }

        public async Task<IEnumerable<SubjectResponseDto>> ListSubjectAsync()
        {
            // Obtiene todas las asignaturas desde el repositorio
            var subjects = await _subjectRepository.ListAsync();

            var list = subjects.ToList(); // Convierte a lista para su manipulación
            List<SubjectResponseDto> Subject_List = new(); // Inicializa una lista para almacenar los DTOs

            // Itera sobre cada asignatura y agrega solo las no eliminadas a la lista de resultados
            for (int i = 0; i < subjects.Count(); i++)
            {
                if (!list[i].IsDeleted)
                    Subject_List.Add(_mapper.Map<SubjectResponseDto>(list[i])); // Mapea y agrega a la lista
            }

            return Subject_List; // Retorna la lista de DTOs de asignaturas no eliminadas
        }

        public async Task<SubjectResponseDto> UpdateSubjectAsync(SubjectResponseDto subjectDto)
        {
            // Obtiene la asignatura por su ID desde el DTO
            var subject = _subjectRepository.GetById(subjectDto.IdSub);

            // Verifica si la asignatura está marcada como eliminada
            if (subject.IsDeleted) return null;

            // Mapea los cambios desde el DTO a la entidad existente
            _mapper.Map(subjectDto, subject);

            // Actualiza la entidad en la base de datos
            await _subjectRepository.UpdateAsync(subject);

            // Mapea y retorna la entidad actualizada como DTO
            return _mapper.Map<SubjectResponseDto>(subject);
        }

    }
}
