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

        public async Task<ClassRoomRestrictionDto> CreateClassRoomRestrictionAsync(ClassRoomRestrictionDto classRoomRestrictionDto)
        {
            var classRoomRestriction = _mapper.Map<Domain.Relations.ClassRoomRestriction>(classRoomRestrictionDto);
            classRoomRestriction.Restriction = await _restrictionRepository.GetByIdAsync(classRoomRestriction.IdRest);
            classRoomRestriction.ClassRoom = await _classRoomRepository.GetByIdAsync(classRoomRestriction.IdClassRoom);
            var savedClassR = await _classRoomRestrictionRepository.CreateAsync(classRoomRestriction);
            return _mapper.Map<ClassRoomRestrictionDto>(savedClassR);
        }

        public async Task<ClassRoomRestrictionDto> DeleteClassRoomRestrictionByIdAsync(int id)
        {
            var classRoomRestriction = _classRoomRestrictionRepository.GetById(id);
            var classRoomRestrictionDto = _mapper.Map<ClassRoomRestrictionDto>(classRoomRestriction);
            await _classRoomRestrictionRepository.DeleteByIdAsync(id);
            return classRoomRestrictionDto;
        }

        public async Task<IEnumerable<ClassRoomRestrictionDto>> ListClassRoomRestrictionsAsync()
        {
            var classRoomRestrictions = await _classRoomRestrictionRepository.ListAsync();
            var list = classRoomRestrictions.ToList();
            List<ClassRoomRestrictionDto> classRoomRestrictionsList = new();

            for (int i = 0; i < list.Count; i++)
            {
                classRoomRestrictionsList.Add(_mapper.Map<ClassRoomRestrictionDto>(list[i]));
            }

            return classRoomRestrictionsList;
        }

        public async Task<ClassRoomRestrictionDto> UpdateClassRoomRestrictionAsync(ClassRoomRestrictionDto classRoomRestrictionDto)
        {
            var classRoomRestriction = await _classRoomRestrictionRepository.GetByIdAsync(classRoomRestrictionDto.IdClassRoomRest);
            _mapper.Map(classRoomRestrictionDto, classRoomRestriction);
            await _classRoomRestrictionRepository.UpdateAsync(classRoomRestriction);
            return _mapper.Map<ClassRoomRestrictionDto>(classRoomRestriction);
        }
    }

}
