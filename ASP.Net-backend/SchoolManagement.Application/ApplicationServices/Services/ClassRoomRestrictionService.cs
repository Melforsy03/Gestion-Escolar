using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomRestriction;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ClassRoomRestrictionService : IClassRoomRestrictionService
    {
        private readonly IClassRoomRestrictionRepository _classRoomRestrictionRepository;
        private readonly IClassRoomRepository _classRoomRepository;
        private readonly IRestrictionRepository _restrictionRepository; 
        private readonly IMapper _mapper;

        public ClassRoomRestrictionService(IClassRoomRestrictionRepository classRoomRestrictionRepository, IClassRoomRepository classRoomRepository, IRestrictionRepository restrictionRepository, IMapper mapper)
        {
            _classRoomRestrictionRepository = classRoomRestrictionRepository;
            _classRoomRepository = classRoomRepository;
            _restrictionRepository = restrictionRepository;
            _mapper = mapper;
        }

        public async Task<ClassRoomRestrictionResponseDto> CreateClassRoomRestrictionAsync(ClassRoomRestrictionDto classRoomRestrictionDto)
        {
            // Mapea el DTO a la entidad de dominio ClassRoomRestriction
            var classRoomRestriction = _mapper.Map<Domain.Relations.ClassRoomRestriction>(classRoomRestrictionDto);

            // Obtiene la restricción correspondiente usando el repositorio
            classRoomRestriction.Restriction = await _restrictionRepository.GetByIdAsync(classRoomRestriction.IdRest);

            // Obtiene el aula correspondiente usando el repositorio
            classRoomRestriction.ClassRoom = await _classRoomRepository.GetByIdAsync(classRoomRestriction.IdClassRoom);

            // Crea la restricción en la base de datos y guarda el resultado
            var savedClassR = await _classRoomRestrictionRepository.CreateAsync(classRoomRestriction);

            // Mapea la entidad guardada de vuelta a un DTO y lo retorna
            return _mapper.Map<ClassRoomRestrictionResponseDto>(savedClassR);
        }

        public async Task<ClassRoomRestrictionResponseDto> DeleteClassRoomRestrictionByIdAsync(int id)
        {
            // Obtiene la restricción del aula por su ID
            var classRoomRestriction = _classRoomRestrictionRepository.GetById(id);

            // Mapea la restricción obtenida a un DTO para la respuesta
            var classRoomRestrictionDto = _mapper.Map<ClassRoomRestrictionResponseDto>(classRoomRestriction);

            // Elimina la restricción del aula por su ID en la base de datos
            await _classRoomRestrictionRepository.DeleteByIdAsync(id);

            // Retorna el DTO de la restricción eliminada
            return classRoomRestrictionDto;
        }

        public async Task<IEnumerable<ClassRoomRestrictionResponseDto>> ListClassRoomRestrictionsAsync()
        {
            // Obtiene todas las restricciones de aulas desde el repositorio
            var classRoomRestrictions = await _classRoomRestrictionRepository.ListAsync();

            // Convierte la lista a una lista para manipulación
            var list = classRoomRestrictions.ToList();

            // Inicializa una lista para almacenar los DTOs de las restricciones de aulas
            List<ClassRoomRestrictionResponseDto> classRoomRestrictionsList = new();

            // Itera sobre cada restricción y mapea a DTOs
            for (int i = 0; i < list.Count; i++)
            {
                classRoomRestrictionsList.Add(_mapper.Map<ClassRoomRestrictionResponseDto>(list[i]));
            }

            // Retorna la lista de DTOs de restricciones de aulas
            return classRoomRestrictionsList;
        }

        public async Task<ClassRoomRestrictionResponseDto> UpdateClassRoomRestrictionAsync(ClassRoomRestrictionResponseDto classRoomRestrictionDto)
        {
            // Obtiene la restricción del aula por su ID desde el DTO
            var classRoomRestriction = await _classRoomRestrictionRepository.GetByIdAsync(classRoomRestrictionDto.IdClassRoomRest);

            // Mapea los cambios desde el DTO a la entidad existente
            _mapper.Map(classRoomRestrictionDto, classRoomRestriction);

            // Actualiza la entidad en la base de datos
            await _classRoomRestrictionRepository.UpdateAsync(classRoomRestriction);

            // Mapea y retorna la entidad actualizada como DTO
            return _mapper.Map<ClassRoomRestrictionResponseDto>(classRoomRestriction);
        }

    }

}
