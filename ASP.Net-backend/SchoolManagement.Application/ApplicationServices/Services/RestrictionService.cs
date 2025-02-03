using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Restriction;
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
    public class RestrictionService : IRestrictionService
    {
        private readonly IRestrictionRepository _restrictionRepository;
        private readonly IMapper _mapper;

        public RestrictionService(IRestrictionRepository restrictionRepository, IMapper mapper)
        {
            // Constructor que inyecta el repositorio de restricciones y el mapeador
            _restrictionRepository = restrictionRepository;
            _mapper = mapper;
        }

        public async Task<RestrictionResponseDto> CreateRestrictionAsync(RestrictionDto restrictionDto)
        {
            // Mapea el DTO de restricción a la entidad de dominio Restriction
            var restriction = _mapper.Map<Domain.Entities.Restriction>(restrictionDto);

            // Crea la restricción en la base de datos y guarda el resultado
            var savedAgency = await _restrictionRepository.CreateAsync(restriction);

            // Mapea la entidad guardada de vuelta a un DTO y lo retorna
            return _mapper.Map<RestrictionResponseDto>(savedAgency);
        }

        public async Task<RestrictionResponseDto> DeleteRestrictionByIdAsync(int restrictionId)
        {
            // Obtiene la restricción por su ID
            var restriction = _restrictionRepository.GetById(restrictionId);

            // Verifica si la restricción ya está marcada como eliminada
            if (restriction.IsDeleted) return null;

            // Marca la restricción como eliminada
            restriction.IsDeleted = true;

            // Actualiza el estado en la base de datos
            await _restrictionRepository.UpdateAsync(restriction);

            // Mapea y retorna el DTO de la restricción eliminada
            return _mapper.Map<RestrictionResponseDto>(restriction);
        }

        public async Task<IEnumerable<RestrictionResponseDto>> ListRestrictionAsync()
        {
            // Obtiene todas las restricciones desde el repositorio
            var restrictions = await _restrictionRepository.ListAsync();

            var list = restrictions.ToList(); // Convierte a lista para su manipulación
            List<RestrictionResponseDto> Restriction_List = new(); // Inicializa una lista para almacenar los DTOs

            // Itera sobre cada restricción y agrega solo las no eliminadas a la lista de resultados
            for (int i = 0; i < restrictions.Count(); i++)
            {
                if (!list[i].IsDeleted)
                    Restriction_List.Add(_mapper.Map<RestrictionResponseDto>(list[i])); // Mapea y agrega a la lista
            }

            return Restriction_List; // Retorna la lista de DTOs de restricciones no eliminadas
        }

        public async Task<RestrictionResponseDto> UpdateRestrictionAsync(RestrictionResponseDto restrictionDto)
        {
            // Obtiene la restricción por su ID desde el DTO
            var restriction = _restrictionRepository.GetById(restrictionDto.IdRes);

            // Verifica si la restricción está marcada como eliminada
            if (restriction.IsDeleted) return null;

            // Mapea los cambios desde el DTO a la entidad existente
            _mapper.Map(restrictionDto, restriction);

            // Actualiza la entidad en la base de datos
            await _restrictionRepository.UpdateAsync(restriction);

            // Mapea y retorna la entidad actualizada como DTO
            return _mapper.Map<RestrictionResponseDto>(restriction);
        }
    }
}

