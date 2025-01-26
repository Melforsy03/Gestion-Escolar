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
            _restrictionRepository = restrictionRepository;
            _mapper = mapper;
        }

        public async Task<RestrictionResponseDto> CreateRestrictionAsync(RestrictionDto restrictionDto)
        {
            var restriction = _mapper.Map<Domain.Entities.Restriction>(restrictionDto);
            var savedAgency = await _restrictionRepository.CreateAsync(restriction);
            return _mapper.Map<RestrictionResponseDto>(savedAgency);
        }

        public async Task<RestrictionResponseDto> DeleteRestrictionByIdAsync(int restrictionId)
        {   
            var restriction = _restrictionRepository.GetById(restrictionId);
            if (restriction.IsDeleted) return null;
            restriction.IsDeleted = true;
            await _restrictionRepository.UpdateAsync(restriction);
            return _mapper.Map<RestrictionResponseDto>(restriction);
        }

        public async Task<IEnumerable<RestrictionResponseDto>> ListRestrictionAsync()
        {
            var restrictions = await _restrictionRepository.ListAsync();
            var list = restrictions.ToList();
            List<RestrictionResponseDto> Restriction_List = new();
            for (int i = 0; i < restrictions.Count(); i++)
            {
                if(!list[i].IsDeleted) Restriction_List.Add(_mapper.Map<RestrictionResponseDto>(list[i]));
            }

            return Restriction_List;
        }

        public async Task<RestrictionResponseDto> UpdateRestrictionAsync(RestrictionResponseDto restrictionDto)
        {
            var restriction = _restrictionRepository.GetById(restrictionDto.IdRes);
            if (restriction.IsDeleted) return null;
            _mapper.Map(restrictionDto, restriction);
            await _restrictionRepository.UpdateAsync(restriction);
            return _mapper.Map<RestrictionResponseDto>(restriction);
        }
    }
}
