using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.SubjectAuxMean;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class SubjectAuxMeanService : ISubjectAuxMeanService
    {
        private readonly ISubjectAuxMeanRepository _subjectAuxMeanRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAuxiliaryMeansRepository _auxiliaryMeansRepository;
        private readonly IMapper _mapper;

        public SubjectAuxMeanService(ISubjectAuxMeanRepository subjectAuxMeanRepository, IMapper mapper, ISubjectRepository subjectRepository, IAuxiliaryMeansRepository auxiliaryMeansRepository)
        {
            _subjectAuxMeanRepository = subjectAuxMeanRepository;
            _mapper = mapper;
            _auxiliaryMeansRepository = auxiliaryMeansRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<SubjectAuxMeanResponseDto> CreateSubjectAuxMeanAsync(SubjectAuxMeanDto subjectAuxMeanDto)
        {
            // Mapea el DTO de la relación asignatura-medios auxiliares a la entidad de dominio SubjectAuxMean
            var subjectAuxMean = _mapper.Map<Domain.Relations.SubjectAuxMean>(subjectAuxMeanDto);

            // Obtiene la asignatura correspondiente utilizando el repositorio
            subjectAuxMean.Subject = await _subjectRepository.GetByIdAsync(subjectAuxMean.IdSub);

            // Obtiene el medio auxiliar correspondiente utilizando el repositorio
            subjectAuxMean.AuxMean = await _auxiliaryMeansRepository.GetByIdAsync(subjectAuxMean.IdAuxMean);

            // Crea la relación en la base de datos y guarda el resultado
            var savedSubjectAuxMean = await _subjectAuxMeanRepository.CreateAsync(subjectAuxMean);

            // Retorna un DTO con los detalles de la relación creada
            return new SubjectAuxMeanResponseDto
            {
                IdAuxMean = savedSubjectAuxMean.IdAuxMean,
                IdSub = savedSubjectAuxMean.IdSub,
                IdSubAuxMean = savedSubjectAuxMean.IdSubAuxMean
            };
        }

        public async Task<SubjectAuxMeanResponseDto> DeleteSubjectAuxMeanByIdAsync(int id)
        {
            // Obtiene la relación asignatura-medios auxiliares por su ID
            var subjectAuxMean = _subjectAuxMeanRepository.GetById(id);

            // Verifica si la relación existe
            if (subjectAuxMean == null) return null; // Retorna null si no se encuentra

            // Crea un DTO de respuesta para la relación que se va a eliminar
            SubjectAuxMeanResponseDto subjectAuxMeanResponse = new SubjectAuxMeanResponseDto
            {
                IdAuxMean = subjectAuxMean.IdAuxMean,
                IdSub = subjectAuxMean.IdSub,
                IdSubAuxMean = subjectAuxMean.IdSubAuxMean
            };

            // Elimina la relación de la base de datos por su ID
            await _subjectAuxMeanRepository.DeleteByIdAsync(id);

            return subjectAuxMeanResponse; // Retorna el DTO de la relación eliminada
        }

        public async Task<IEnumerable<SubjectAuxMeanResponseDto>> ListSubjectAuxMeansAsync()
        {
            // Obtiene todas las relaciones entre asignaturas y medios auxiliares desde el repositorio
            var subjectAuxMeans = await _subjectAuxMeanRepository.ListAsync();

            var list = subjectAuxMeans.ToList(); // Convierte a lista para su manipulación
            List<SubjectAuxMeanResponseDto> subjectAuxMeansList = new(); // Inicializa una lista para almacenar los DTOs

            // Itera sobre cada relación y crea un DTO para cada una de ellas
            for (int i = 0; i < list.Count; i++)
            {
                subjectAuxMeansList.Add(new SubjectAuxMeanResponseDto
                {
                    IdAuxMean = list[i].IdAuxMean,
                    IdSub = list[i].IdSub,
                    IdSubAuxMean = list[i].IdSubAuxMean
                }); // Agrega el DTO a la lista
            }

            return subjectAuxMeansList; // Retorna la lista de DTOs de relaciones entre asignaturas y medios auxiliares
        }

        public async Task<SubjectAuxMeanResponseDto> UpdateSubjectAuxMeanAsync(SubjectAuxMeanResponseDto subjectAuxMeanResponseDto)
        {
            // Obtiene la relación asignatura-medios auxiliares por su ID desde el DTO
            var subjectAuxMean = await _subjectAuxMeanRepository.GetByIdAsync(subjectAuxMeanResponseDto.IdSubAuxMean);

            // Mapea los cambios desde el DTO a la entidad existente
            _mapper.Map(subjectAuxMeanResponseDto, subjectAuxMean);

            // Actualiza la entidad en la base de datos
            await _subjectAuxMeanRepository.UpdateAsync(subjectAuxMean);

            // Retorna un nuevo DTO con los detalles actualizados de la relación
            return new SubjectAuxMeanResponseDto
            {
                IdSubAuxMean = subjectAuxMean.IdSubAuxMean,
                IdAuxMean = subjectAuxMean.IdAuxMean,
                IdSub = subjectAuxMean.IdSub
            };
        }
    }
}

