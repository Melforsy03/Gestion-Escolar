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
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ResponseDto.Administrator;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public AdministratorService(IAdministratorRepository administratorRepository, IMapper mapper, Triggers trigger)
        {
            _administratorRepository = administratorRepository;
            _mapper = mapper;
            _trigger = trigger;
        }

        public async Task<AdministratorCreateResponseDto> CreateAdministratorAsync(AdministratorDto administratorDto)
        {
            var administrator = _mapper.Map<Administrator>(administratorDto);
            (User, string) User = await _trigger.RegisterUser(administratorDto.AdminName, "Admin");
            administrator.UserId = User.Item1.Id;
            var savedAdministrator = await _administratorRepository.CreateAsync(administrator);
            AdministratorCreateResponseDto answer = new AdministratorCreateResponseDto();
            answer.Id = administrator.AdminId;
            answer.Administrator = _mapper.Map<AdministratorDto>(savedAdministrator);
            answer.UserName = User.Item1.UserName;
            answer.Password = User.Item2;
            return answer;
        }

        public async Task<AdministratorResponseDto> DeleteAdministratorByIdAsync(int administratorId)
        {
            var administrator = _administratorRepository.GetById(administratorId);
            if (administrator.IsDeleted)
            {
                return null;
            }
            administrator.IsDeleted = true;
            await _administratorRepository.UpdateAsync(administrator);
            AdministratorResponseDto answer = new AdministratorResponseDto();
            answer.Id = administrator.AdminId;
            answer.Administrator = _mapper.Map<AdministratorDto>(administrator);
            return answer;
        }

        public async Task<IEnumerable<AdministratorResponseDto>> ListAdministratorsAsync()
        {
            var administrators = await _administratorRepository.ListAsync();
            var list = administrators.ToList();
            List<AdministratorResponseDto> Administrators_List = new List<AdministratorResponseDto>();

            for (int i = 0; i < administrators.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    AdministratorResponseDto answer = new AdministratorResponseDto();
                    answer.Id = list[i].AdminId;
                    answer.Administrator = _mapper.Map<AdministratorDto>(list[i]);
                    Administrators_List.Add(answer);
                }
            }

            return Administrators_List;
        }

        public async Task<AdministratorResponseDto> UpdateAdministratorAsync(AdministratorResponseDto administratorInfo)
        {
            var administrator = _administratorRepository.GetById(administratorInfo.Id);
            if (administrator.IsDeleted)
            {
                return null;
            }
            _mapper.Map(administratorInfo.Administrator, administrator);
            await _administratorRepository.UpdateAsync(administrator);
            AdministratorResponseDto answer = new AdministratorResponseDto();
            answer.Id = administrator.AdminId;
            answer.Administrator = _mapper.Map<AdministratorDto>(administrator);
            return answer;
        }
    }

}
