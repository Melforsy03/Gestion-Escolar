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
        public async Task<ClassRoomTechMeanAmmount> GetClassRoomTechAmmount()
        {
            var classRoom = _context.ClassRooms.ToList();
            ClassRoomTechMeanAmmount classRoomTechMeanAmmount = new ClassRoomTechMeanAmmount();
            foreach(var cr in classRoom)
            {
                var classRoomTechMean = _context.ClassRoomTechMeans.Where(crtm => crtm.IdClassRoom == cr.IdClassR);
                List<TechnologicalMeans> techMean = new List<TechnologicalMeans>();
                foreach(var crtm in classRoomTechMean)
                {
                    techMean.AddRange(_context.TechnologicalMeans.Where(tm => tm.IdMean == crtm.IdTechMean).ToList());
                }
                List<int> costs = new List<int>();
                foreach(var tm in techMean)
                {
                    DateTime dateLimit = DateTime.Now.AddYears(-1);
                    var maintenances = _context.Maintenances.Where(m => m.IdTechMean == tm.IdMean && m.MaintenanceDate >= DateOnly.FromDateTime(dateLimit)).ToList();
                    
                    foreach (var main in maintenances)
                    {
                            costs.Add(main.Cost);
                    }
                    
                    

                }
                if(costs.Count() > 2)
                {
                    classRoomTechMeanAmmount.ClassRoomAverageCost.Add(cr.IdClassR, _trigger.GetAverage(costs));
                }

            }

            return classRoomTechMeanAmmount;
        }

        public async Task<ClassRoomMeanAmmount> GetClassRoomsMeanAmmount()
        {
            var maintenanceTech = _context.Maintenances.Where(m => m.typeOfMean == 0);
            var maintenanceAux = _context.Maintenances.Where(m => m.typeOfMean == 1);
            var classRooms = _context.ClassRooms;
           

            Dictionary<int, Dictionary<string, int>> ClassRoomMaintenanceByType = new Dictionary<int, Dictionary<string, int>>();

            foreach(var cr in classRooms)
            {
                int auxMeanAmmount = 0;
                int techMeanAmmount = 0;
                var classRoomTechMean = _context.ClassRoomTechMeans.Where(ctm => ctm.IdClassRoom == cr.IdClassR);

                Dictionary<string, int> temp = new Dictionary<string, int>();
                foreach (var crtm in classRoomTechMean)
                {
                    var maintenancesForThisMean = maintenanceTech.Where(m => m.IdTechMean == crtm.IdTechMean).ToList();
                    techMeanAmmount += maintenancesForThisMean.Count();
                }
                temp.Add("TechnologicalMean", techMeanAmmount);
                var subjectsInCR = _context.Subjects.Where(s => s.IdClassRoom == cr.IdClassR).ToList();
                List<SubjectAuxMean> list = new List<SubjectAuxMean>();
                List<AuxiliaryMeans> auxMean = new List<AuxiliaryMeans>();
                
                foreach(var sub in subjectsInCR)
                {
                    list.AddRange(_context.SubjectAuxMeans.Where(sam => sam.IdSub == sub.IdSub).ToList());
                }

                foreach(var subam in list)
                {
                    auxMean.Add(_context.AuxiliaryMeans.Where(am => am.IdMean == subam.IdAuxMean).FirstOrDefault());
                }
                
                foreach(var am in auxMean)
                {
                    var maintenancesForThisMean = maintenanceAux.Where(ma => ma.IdAuxMean == am.IdMean).ToList();
                    auxMeanAmmount += maintenancesForThisMean.Count();
                }
                temp.Add("AuxiliaryMean", auxMeanAmmount);

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
