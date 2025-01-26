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
            var classRoomRestriction = _mapper.Map<Domain.Relations.ClassRoomRestriction>(classRoomRestrictionDto);
            classRoomRestriction.Restriction = await _restrictionRepository.GetByIdAsync(classRoomRestriction.IdRest);
            classRoomRestriction.ClassRoom = await _classRoomRepository.GetByIdAsync(classRoomRestriction.IdClassRoom);
            var savedClassR = await _classRoomRestrictionRepository.CreateAsync(classRoomRestriction);
            return _mapper.Map<ClassRoomRestrictionResponseDto>(savedClassR);
        }

        public async Task<ClassRoomRestrictionResponseDto> DeleteClassRoomRestrictionByIdAsync(int id)
        {
            var classRoomRestriction = _classRoomRestrictionRepository.GetById(id);
            var classRoomRestrictionDto = _mapper.Map<ClassRoomRestrictionResponseDto>(classRoomRestriction);
            await _classRoomRestrictionRepository.DeleteByIdAsync(id);
            return classRoomRestrictionDto;
        }

        public async Task<IEnumerable<ClassRoomRestrictionResponseDto>> ListClassRoomRestrictionsAsync()
        {
            var classRoomRestrictions = await _classRoomRestrictionRepository.ListAsync();
            var list = classRoomRestrictions.ToList();
            List<ClassRoomRestrictionResponseDto> classRoomRestrictionsList = new();

            for (int i = 0; i < list.Count; i++)
            {
                classRoomRestrictionsList.Add(_mapper.Map<ClassRoomRestrictionResponseDto>(list[i]));
            }

            return classRoomRestrictionsList;
        }

        public async Task<ClassRoomRestrictionResponseDto> UpdateClassRoomRestrictionAsync(ClassRoomRestrictionResponseDto classRoomRestrictionDto)
        {
            var classRoomRestriction = await _classRoomRestrictionRepository.GetByIdAsync(classRoomRestrictionDto.IdClassRoomRest);
            _mapper.Map(classRoomRestrictionDto, classRoomRestriction);
            await _classRoomRestrictionRepository.UpdateAsync(classRoomRestriction);
            return _mapper.Map<ClassRoomRestrictionResponseDto>(classRoomRestriction);
        }
    }

}
