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
    public class RestrictionService : IRestrictionService
    {
        private readonly IRestrictionRepository _restrictionRepository;
        private readonly IMapper _mapper;

        public RestrictionService(IRestrictionRepository restrictionRepository, IMapper mapper)
        {
            _restrictionRepository = restrictionRepository;
            _mapper = mapper;
        }

        public async Task<RestrictionDto> CreateRestrictionAsync(RestrictionDto restrictionDto)
        {
            var restriction = _mapper.Map<Domain.Entities.Restriction>(restrictionDto);
            var savedAgency = await _restrictionRepository.CreateAsync(restriction);
            return _mapper.Map<RestrictionDto>(savedAgency);
        }

        public async Task DeleteRestrictionByIdAsync(int restrictionDto)
        {
            await _restrictionRepository.DeleteByIdAsync(restrictionDto);
        }

        public async Task<IEnumerable<RestrictionDto>> ListRestrictionAsync()
        {
            var restrictions = await _restrictionRepository.ListAsync();
            var list = restrictions.ToList();
            List<RestrictionDto> Restriction_List = new();
            for (int i = 0; i < restrictions.Count(); i++)
            {
                Restriction_List.Add(_mapper.Map<RestrictionDto>(list[i]));
            }

            return Restriction_List;
        }

        public async Task<RestrictionDto> UpdateRestrictionAsync(RestrictionDto restrictionDto)
        {
            var restriction = _restrictionRepository.GetById(restrictionDto.IdRes);
            _mapper.Map(restrictionDto, restriction);
            await _restrictionRepository.UpdateAsync(restriction);
            return _mapper.Map<RestrictionDto>(restriction);
        }
    }
}
