using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
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
        private readonly IMapper _mapper;

        public ClassRoomService(IClassRoomRepository classRoomRepository, IMapper mapper)
        {
            _classRoomRepository = classRoomRepository;
            _mapper = mapper;
        }

        public async Task<ClassRoomDto> CreateClassRoomAsync(ClassRoomDto classRoomDto)
        {
            var classRoom = _mapper.Map<Domain.Entities.ClassRoom>(classRoomDto);
            var savedClassR = await _classRoomRepository.CreateAsync(classRoom);
            return _mapper.Map<ClassRoomDto>(savedClassR);
        }

        public async Task<ClassRoomDto> DeleteClassRoomByIdAsync(int ClassRoomDto)
        {
            var classRoom = _classRoomRepository.GetById(ClassRoomDto);
            if (classRoom.IsDeleted)
            {
                return null;
            }
            classRoom.IsDeleted = true;
            await _classRoomRepository.UpdateAsync(classRoom);
            return _mapper.Map<ClassRoomDto>(classRoom);
        }

        public async Task<IEnumerable<ClassRoomDto>> ListClassRoomAsync()
        {
            var classRooms = await _classRoomRepository.ListAsync();
            var list = classRooms.ToList();
            List<ClassRoomDto> classRooms_List = new();
            for (int i = 0; i < classRooms.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    classRooms_List.Add(_mapper.Map<ClassRoomDto>(list[i]));
                }
            }

            return classRooms_List;
        }

        public async Task<ClassRoomDto> UpdateClassRoomAsync(ClassRoomDto ClassRoomDto)
        {
            var ClassRoom = _classRoomRepository.GetById(ClassRoomDto.IdClassR);
            if (ClassRoom.IsDeleted) return null;
            _mapper.Map(ClassRoomDto, ClassRoom);
            await _classRoomRepository.UpdateAsync(ClassRoom);
            return _mapper.Map<ClassRoomDto>(ClassRoom);
        }
    }
}
