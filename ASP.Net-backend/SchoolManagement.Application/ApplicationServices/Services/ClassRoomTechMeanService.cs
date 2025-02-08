using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomTechMean;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ClassRoomTechMeanService : IClassRoomTechMeanService
    {
        private readonly IClassRoomTechMeanRepository _classRoomTechMeanRepository;
        private readonly Context _context;
        private readonly IClassRoomRepository _classRoomRepository;
        private readonly ITechnologicalMeansRepository _technologicalMeansRepository;
        private readonly IMapper _mapper;

        public ClassRoomTechMeanService(Context contex, IClassRoomTechMeanRepository classRoomTechMeanRepository, IClassRoomRepository classRoomRepository, ITechnologicalMeansRepository technologicalMeansRepository,IMapper mapper)
        {
            _classRoomTechMeanRepository = classRoomTechMeanRepository;
            _context = contex;
            _classRoomRepository = classRoomRepository;
            _technologicalMeansRepository = technologicalMeansRepository;
            _mapper = mapper;
        }

        public async Task<ClassRoomTechMeanResponseDto> CreateClassRoomTechMeanAsync(ClassRoomTechMeanDto classRoomTechMeanDto)
        {
            // Mapea el DTO a la entidad de dominio ClassRoomTechMean
            var classRoomTechMean = _mapper.Map<Domain.Relations.ClassRoomTechMean>(classRoomTechMeanDto);

            // Obtiene el medio tecnológico correspondiente usando el repositorio
            classRoomTechMean.TechnologicalMeans = await _technologicalMeansRepository.GetByIdAsync(classRoomTechMean.IdTechMean);

            // Obtiene el aula correspondiente usando el repositorio
            classRoomTechMean.ClassRoom = await _classRoomRepository.GetByIdAsync(classRoomTechMean.IdClassRoom);

            // Crea la relación en la base de datos y guarda el resultado
            var savedClassR = await _classRoomTechMeanRepository.CreateAsync(classRoomTechMean);

            // Mapea la entidad guardada de vuelta a un DTO y lo retorna
            return _mapper.Map<ClassRoomTechMeanResponseDto>(savedClassR);
        }

        public async Task<ClassRoomTechMeanResponseDto> DeleteClassRoomTechMeanByIdAsync(int id)
        {
            // Obtiene la relación de aula y medio tecnológico por su ID
            var classRoomTeachMean = _classRoomTechMeanRepository.GetById(id);

            // Mapea la relación obtenida a un DTO para la respuesta
            var classRoomTechMeanDto = _mapper.Map<ClassRoomTechMeanResponseDto>(classRoomTeachMean);

            // Elimina la relación de aula y medio tecnológico por su ID en la base de datos
            await _classRoomTechMeanRepository.DeleteByIdAsync(id);

            // Retorna el DTO de la relación eliminada
            return classRoomTechMeanDto;
        }

        public async Task<IEnumerable<ClassRoomTechMeanResponseDto>> ListClassRoomTechMeansAsync()
        {
            // Obtiene todas las relaciones de aulas y medios tecnológicos desde el repositorio
            var classRoomTechMeans = await _classRoomTechMeanRepository.ListAsync();

            var list = classRoomTechMeans.ToList(); // Convierte a lista para su manipulación
            List<ClassRoomTechMeanResponseDto> classRoomTechMeansList = new(); // Inicializa una lista para almacenar los DTOs

            // Itera sobre cada relación y mapea a DTOs
            for (int i = 0; i < list.Count; i++)
            {
                var temp = _mapper.Map<ClassRoomTechMeanResponseDto>(list[i]); // Mapea a DTO

                // Obtiene el nombre del medio tecnológico asociado a la relación actual
                temp.TechName = _context.TechnologicalMeans.Where(tm => tm.IdMean == list[i].IdTechMean).FirstOrDefault().NameMean;

                // Agrega el DTO a la lista de resultados
                classRoomTechMeansList.Add(temp);
            }

            // Retorna la lista de DTOs de relaciones entre aulas y medios tecnológicos
            return classRoomTechMeansList;
        }

        public async Task<ClassRoomTechMeanResponseDto> UpdateClassRoomTechMeanAsync(ClassRoomTechMeanResponseDto classRoomTechMeanDto)
        {
            // Obtiene la relación de aula y medio tecnológico por su ID desde el DTO
            var classRoomTechMean = await _classRoomTechMeanRepository.GetByIdAsync(classRoomTechMeanDto.IdClassRoomTech);

            // Mapea los cambios desde el DTO a la entidad existente
            _mapper.Map(classRoomTechMeanDto, classRoomTechMean);

            // Actualiza la entidad en la base de datos
            await _classRoomTechMeanRepository.UpdateAsync(classRoomTechMean);

            // Mapea y retorna la entidad actualizada como DTO
            return _mapper.Map<ClassRoomTechMeanResponseDto>(classRoomTechMean);
        }

    }

}
