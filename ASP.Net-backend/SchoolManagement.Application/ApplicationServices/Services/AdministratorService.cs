using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
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

        public async Task<AdministratorDto> DeleteAdministratorByIdAsync(int administratorId)
        {
            var administrator = _administratorRepository.GetById(administratorId);
            if (administrator.IsDeleted)
            {
                return null;
            }
            administrator.IsDeleted = true;
            await _administratorRepository.UpdateAsync(administrator);
            return _mapper.Map<AdministratorDto>(administrator);
        }


        public async Task<IEnumerable<AdministratorDto>> ListAdministratorAsync()
        {
            var administrators = await _administratorRepository.ListAsync();
            var list = administrators.ToList();
            List<AdministratorDto> Administrators_List = new();
            for (int i = 0; i < administrators.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    Administrators_List.Add(_mapper.Map<AdministratorDto>(list[i]));
                }
            }

            return Administrators_List;
        }

        public async Task<AdministratorDto> UpdateAdministratorAsync(AdministratorDto administratorDto)
        {
            var administrator = _administratorRepository.GetById(administratorDto.AdminId);
            if (!administrator.IsDeleted)
            {
                return null;
            }
            _mapper.Map(administratorDto, administrator);
            await _administratorRepository.UpdateAsync(administrator);
            return _mapper.Map<AdministratorDto>(administrator);
        }


    }
}
