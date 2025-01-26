using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.Common;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.Identity;
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
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public AdministratorService(IAdministratorRepository administratorRepository, IMapper mapper, Triggers triggers)
        {
            _administratorRepository = administratorRepository;
            _trigger = triggers;
            _mapper = mapper;
        }

        public async Task<(int Id, AdministratorDto administrator, string UserName, string Password)> CreateAdministratorAsync(AdministratorDto administratorDto)
        {
            var administrator = _mapper.Map<Administrator>(administratorDto);
            (User, string) User = await _trigger.RegisterUser(administratorDto.AdminName, "Admin");
            administrator.UserId = User.Item1.Id;
            var savedAdministrator = await _administratorRepository.CreateAsync(administrator);

            administratorDto = _mapper.Map<AdministratorDto>(savedAdministrator);

            return (administrator.AdminId, administratorDto, User.Item1.UserName, User.Item2);
        }

        public async Task<(int Id, AdministratorDto administrator)> DeleteAdministratorByIdAsync(int administratorId)
        {
            var administrator = _administratorRepository.GetById(administratorId);
            if (administrator.IsDeleted)
            {
                return (0, null);
            }
            administrator.IsDeleted = true;
            await _administratorRepository.UpdateAsync(administrator);
            return (administrator.AdminId, _mapper.Map<AdministratorDto>(administrator));
        }


        public async Task<IEnumerable<(int Id, AdministratorDto administrator)>> ListAdministratorAsync()
        {
            var administrators = await _administratorRepository.ListAsync();
            var list = administrators.ToList();
            List<(int Id, AdministratorDto administrator)> Administrators_List = new();
            for (int i = 0; i < administrators.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    Administrators_List.Add((list[i].AdminId, _mapper.Map<AdministratorDto>(list[i])));
                }
            }

            return Administrators_List;
        }

        public async Task<(int Id, AdministratorDto administrator)> UpdateAdministratorAsync((int Id, AdministratorDto administratorDto) administratorInfo)
        {
            var administrator = _administratorRepository.GetById(administratorInfo.Id);
            if (!administrator.IsDeleted)
            {
                return (0, null);
            }
            _mapper.Map(administratorInfo.administratorDto, administrator);
            await _administratorRepository.UpdateAsync(administrator);
            return (administrator.AdminId, _mapper.Map<AdministratorDto>(administrator));
        }


    }
}
