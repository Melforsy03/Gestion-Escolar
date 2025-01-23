using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IMapper _mapper;

        public AdministratorService(IAdministratorRepository administratorRepository, IMapper mapper)
        {
            _administratorRepository = administratorRepository;
            _mapper = mapper;
        }

        public async Task<AdministratorDto> CreateAdministratorAsync(AdministratorDto administratorDto)
        {
            var administrator = _mapper.Map<Domain.Entities.Administrator>(administratorDto);
            var savedAdministrator = await _administratorRepository.CreateAsync(administrator);
            return _mapper.Map<AdministratorDto>(savedAdministrator);
        }

        public async Task DeleteAdministratorByIdAsync(int administratorId)
        {
            await _administratorRepository.DeleteByIdAsync(administratorId);
        }

        public async Task<IEnumerable<AdministratorDto>> ListAdministratorAsync()
        {
            var administratorList = await _administratorRepository.ListAsync();
            var list = administratorList.ToList();
            List<AdministratorDto> administratorDtos = new();
            for (int i = 0; i < administratorList.Count(); i++)
            {
                administratorDtos.Add(_mapper.Map<AdministratorDto>(list[i]));
            }

            return administratorDtos;
        }

        public async Task<AdministratorDto> UpdateAdministratorAsync(AdministratorDto administratorDto)
        {
            var administrator = _administratorRepository.GetById(administratorDto.AdminId);
            _mapper.Map(administratorDto, administrator);
            await _administratorRepository.UpdateAsync(administrator);
            return _mapper.Map<AdministratorDto>(administrator);
        }
    }

}
