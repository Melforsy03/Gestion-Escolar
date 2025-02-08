using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoom;
using SchoolManagement.Application.Common;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ClassRoomService : IClassRoomService
    {
        private readonly IClassRoomRepository _classRoomRepository;
        private readonly Context _context;
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public ClassRoomService(Triggers trigger, Context context, IClassRoomRepository classRoomRepository, IMapper mapper)
        {
            _classRoomRepository = classRoomRepository;
            _context = context;
            _mapper = mapper;
            _trigger = trigger;
        }

        public async Task<ClassRoomResponseDto> CreateClassRoomAsync(ClassRoomDto classRoomDto)
        {
            // Mapea el DTO a la entidad de dominio ClassRoom
            var classRoom = _mapper.Map<Domain.Entities.ClassRoom>(classRoomDto);

            // Crea el aula en la base de datos y guarda el resultado
            var savedClassR = await _classRoomRepository.CreateAsync(classRoom);

            // Mapea la entidad guardada de vuelta a un DTO y lo retorna
            return _mapper.Map<ClassRoomResponseDto>(savedClassR);
        }

        public async Task<ClassRoomResponseDto> DeleteClassRoomByIdAsync(int classRoomId)
        {
            // Obtiene el aula por su ID
            var classRoom = _classRoomRepository.GetById(classRoomId);

            // Verifica si el aula ya está marcada como eliminada
            if (classRoom.IsDeleted)
            {
                return null; // Retorna null si ya está eliminada
            }

            // Marca el aula como eliminada
            classRoom.IsDeleted = true;

            // Actualiza el estado en la base de datos
            await _classRoomRepository.UpdateAsync(classRoom);

            // Mapea y retorna el aula eliminada como DTO
            return _mapper.Map<ClassRoomResponseDto>(classRoom);
        }

        public async Task<ClassRoomTechMeanAmmount> GetClassRoomTechAmmount()
        {
            // Obtiene todas las aulas desde el contexto
            var classRooms = _context.ClassRooms.ToList();

            // Inicializa un objeto para almacenar los costos promedio de las aulas
            ClassRoomTechMeanAmmount classRoomTechMeanAmmount = new ClassRoomTechMeanAmmount();

            // Inicializa un diccionario para almacenar el costo promedio por aula
            classRoomTechMeanAmmount.ClassRoomAverageCost = new Dictionary<int, float>();

            // Itera sobre cada aula para calcular su costo promedio tecnológico
            foreach (var cr in classRooms)
            {
                // Obtiene los medios tecnológicos asociados a la aula actual
                var classRoomTechMean = _context.ClassRoomTechMeans.Where(crtm => crtm.IdClassRoom == cr.IdClassR);

                List<TechnologicalMeans> techMeans = new List<TechnologicalMeans>();

                // Carga los medios tecnológicos en una lista
                foreach (var crtm in classRoomTechMean)
                {
                    techMeans.AddRange(_context.TechnologicalMeans.Where(tm => tm.IdMean == crtm.IdTechMean).ToList());
                }

                List<int> costs = new List<int>();

                // Itera sobre los medios tecnológicos para obtener sus costos de mantenimiento
                foreach (var tm in techMeans)
                {
                    DateTime dateLimit = DateTime.Now.AddYears(-1); // Define un límite de un año atrás

                    // Obtiene los mantenimientos realizados en el último año para el medio tecnológico actual
                    var maintenances = _context.Maintenances.Where(m => m.IdTechMean == tm.IdMean && m.MaintenanceDate >= DateOnly.FromDateTime(dateLimit)).ToList();

                    foreach (var main in maintenances)
                    {
                        costs.Add(main.Cost); // Agrega el costo de mantenimiento a la lista
                    }
                }

                // Calcula y agrega el costo promedio al diccionario si hay más de dos costos registrados
                if (costs.Count() > 2)
                {
                    classRoomTechMeanAmmount.ClassRoomAverageCost.Add(cr.IdClassR, _trigger.GetAverage(costs));
                }
            }

            return classRoomTechMeanAmmount; // Retorna el objeto con los costos promedio por aula
        }

        public async Task<ClassRoomMeanAmmount> GetClassRoomsMeanAmmount()
        {
            // Obtiene mantenimientos de medios tecnológicos y auxiliares desde el contexto
            var maintenanceTech = _context.Maintenances.Where(m => m.typeOfMean == 0);
            var maintenanceAux = _context.Maintenances.Where(m => m.typeOfMean == 1);

            // Obtiene todas las aulas desde el contexto
            var classRooms = _context.ClassRooms;

            // Inicializa un diccionario para almacenar mantenimientos por tipo por cada aula
            Dictionary<int, Dictionary<string, int>> ClassRoomMaintenanceByType = new Dictionary<int, Dictionary<string, int>>();

            foreach (var cr in classRooms)
            {
                int auxMeanAmount = 0; // Contador para medios auxiliares
                int techMeanAmount = 0; // Contador para medios tecnológicos

                // Obtiene los medios tecnológicos asociados a la aula actual
                var classRoomTechMean = _context.ClassRoomTechMeans.Where(ctm => ctm.IdClassRoom == cr.IdClassR);

                Dictionary<string, int> temp = new Dictionary<string, int>(); // Diccionario temporal para almacenar cantidades

                foreach (var crtm in classRoomTechMean)
                {
                    var maintenancesForThisMean = maintenanceTech.Where(m => m.IdTechMean == crtm.IdTechMean).ToList();
                    techMeanAmount += maintenancesForThisMean.Count(); // Suma la cantidad de mantenimientos tecnológicos
                }

                temp.Add("TechnologicalMean", techMeanAmount); // Agrega al diccionario temporal

                // Obtiene las materias asociadas a la aula actual
                var subjectsInCR = _context.Subjects.Where(s => s.IdClassRoom == cr.IdClassR).ToList();

                List<SubjectAuxMean> list = new List<SubjectAuxMean>();
                List<AuxiliaryMeans> auxMeans = new List<AuxiliaryMeans>();

                foreach (var sub in subjectsInCR)
                {
                    list.AddRange(_context.SubjectAuxMeans.Where(sam => sam.IdSub == sub.IdSub).ToList());
                }

                foreach (var subam in list)
                {
                    auxMeans.Add(_context.AuxiliaryMeans.Where(am => am.IdMean == subam.IdAuxMean).FirstOrDefault());
                }

                foreach (var am in auxMeans)
                {
                    var maintenancesForThisMean = maintenanceAux.Where(ma => ma.IdAuxMean == am.IdMean).ToList();
                    auxMeanAmount += maintenancesForThisMean.Count(); // Suma la cantidad de mantenimientos auxiliares
                }

                temp.Add("AuxiliaryMean", auxMeanAmount); // Agrega al diccionario temporal

                ClassRoomMaintenanceByType.Add(cr.IdClassR, temp); // Agrega la información del aula al diccionario general
            }

            DateTime limitDate = DateTime.Now.AddYears(-2); // Define un límite de dos años atrás

            return new ClassRoomMeanAmmount
            {
                ClassRoomsAndMeans = ClassRoomMaintenanceByType,
                AmmountOfMaintenance2yo = _context.Maintenances.Count(m => m.MaintenanceDate >= DateOnly.FromDateTime(limitDate))
            }; // Retorna un objeto con la información recopilada sobre mantenimientos
        }

        public async Task<IEnumerable<ClassRoomResponseDto>> ListClassRoomAsync()
        {
            // Obtiene todas las aulas desde el repositorio
            var classRooms = await _classRoomRepository.ListAsync();

            var list = classRooms.ToList();
            List<ClassRoomResponseDto> classRooms_List = new();

            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].IsDeleted)
                {
                    classRooms_List.Add(_mapper.Map<ClassRoomResponseDto>(list[i])); // Mapea y agrega las aulas no eliminadas a la lista de respuesta
                }
            }

            return classRooms_List; // Retorna la lista de aulas no eliminadas como DTOs
        }

        public async Task<ClassRoomResponseDto> UpdateClassRoomAsync(ClassRoomResponseDto classRoomDto)
        {
            // Obtiene el aula por su ID desde el DTO
            var classRoom = _classRoomRepository.GetById(classRoomDto.IdClassR);

            if (classRoom.IsDeleted) return null; // Retorna null si el aula está eliminada

            _mapper.Map(classRoomDto, classRoom); // Mapea los cambios desde el DTO a la entidad existente

            await _classRoomRepository.UpdateAsync(classRoom); // Actualiza la entidad en la base de datos

            return _mapper.Map<ClassRoomResponseDto>(classRoom); // Mapea y retorna la entidad actualizada como DTO
        }

    }
}
