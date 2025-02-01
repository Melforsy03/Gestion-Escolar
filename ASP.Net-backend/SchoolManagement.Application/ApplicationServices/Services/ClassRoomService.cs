using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoom;
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
        private readonly IMapper _mapper;

        public ClassRoomService(Context context, IClassRoomRepository classRoomRepository, IMapper mapper)
        {
            _classRoomRepository = classRoomRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClassRoomResponseDto> CreateClassRoomAsync(ClassRoomDto classRoomDto)
        {
            var classRoom = _mapper.Map<Domain.Entities.ClassRoom>(classRoomDto);
            var savedClassR = await _classRoomRepository.CreateAsync(classRoom);
            return _mapper.Map<ClassRoomResponseDto>(savedClassR);
        }

        public async Task<ClassRoomResponseDto> DeleteClassRoomByIdAsync(int ClassRoomDto)
        {
            var classRoom = _classRoomRepository.GetById(ClassRoomDto);
            if (classRoom.IsDeleted)
            {
                return null;
            }
            classRoom.IsDeleted = true;
            await _classRoomRepository.UpdateAsync(classRoom);
            return _mapper.Map<ClassRoomResponseDto>(classRoom);
        }

        public async Task<ClassRoomMeanAmmount> GetClassRoomsMeanAmmount()
        {
            var maintenance = _context.Maintenances.Where(m => m.typeOfMean == 0);
            var classRooms = _context.ClassRooms;
            Dictionary<int, Dictionary<string, string>> ClassRoomMaintenanceByType = new Dictionary<int, Dictionary<string, string>>();
            foreach(var cr in classRooms)
            {
                var classRoomTechMean = _context.ClassRoomTechMeans.Where(ctm => ctm.IdClassRoom == cr.IdClassR);

                Dictionary<string, string> temp = new Dictionary<string, string>();
                foreach (var crtm in classRoomTechMean)
                {
                    var maintenancesForThisMean = maintenance.Where(m => m.IdTechMean == crtm.IdTechMean);
                    if(maintenancesForThisMean.Count() > 0)
                    {
                        foreach (var maint in maintenancesForThisMean)
                        {
                            var mean = _context.TechnologicalMeans.Where(tm => tm.IdMean == maint.IdTechMean).First();
                            temp.Add(mean.NameMean, "TechnologicalMean");
                        }

                    }
                }

                ClassRoomMaintenanceByType.Add(cr.IdClassR, temp);
            }
            DateTime Limit = DateTime.Now.AddYears(-2);
            return new ClassRoomMeanAmmount { ClassRoomsAndMeans = ClassRoomMaintenanceByType , AmmountOfMaintenance2yo = _context.Maintenances.Count(m => m.MaintenanceDate >= DateOnly.FromDateTime(Limit)) };
        }

        public async Task<IEnumerable<ClassRoomResponseDto>> ListClassRoomAsync()
        {
            var classRooms = await _classRoomRepository.ListAsync();
            var list = classRooms.ToList();
            List<ClassRoomResponseDto> classRooms_List = new();
            for (int i = 0; i < classRooms.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    classRooms_List.Add(_mapper.Map<ClassRoomResponseDto>(list[i]));
                }
            }

            return classRooms_List;
        }

        public async Task<ClassRoomResponseDto> UpdateClassRoomAsync(ClassRoomResponseDto ClassRoomDto)
        {
            var ClassRoom = _classRoomRepository.GetById(ClassRoomDto.IdClassR);
            if (ClassRoom.IsDeleted) return null;
            _mapper.Map(ClassRoomDto, ClassRoom);
            await _classRoomRepository.UpdateAsync(ClassRoom);
            return _mapper.Map<ClassRoomResponseDto>(ClassRoom);
        }
    }
}
