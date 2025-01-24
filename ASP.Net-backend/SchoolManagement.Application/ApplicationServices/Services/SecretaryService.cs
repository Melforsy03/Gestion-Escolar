using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.Common;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class SecretaryService : ISecretaryService
    {
        private readonly ISecretaryRepository _secretaryRepository;
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public SecretaryService(ISecretaryRepository secretaryRepository, IMapper mapper, Triggers trigger)
        {
            _secretaryRepository = secretaryRepository;
            _mapper = mapper;
            _trigger = trigger;
        }

        public async Task<SecretaryDto> CreateSecretaryAsync(SecretaryDto secretaryDto)
        {
            var secretary = _mapper.Map<Secretary>(secretaryDto);
            User User = await _trigger.RegisterUser(secretaryDto.NameS, "Secretary");
            secretary.UserId = User.Id;
            var savedSecretary = await _secretaryRepository.CreateAsync(secretary);

            secretaryDto = _mapper.Map<SecretaryDto>(savedSecretary);
            secretaryDto.UserName = User.UserName;
            secretaryDto.PasswordHash = User.PasswordHash;
            return secretaryDto;
        }

        public async Task<SecretaryDto> DeleteSecretaryByIdAsync(int secretaryId)
        {
            var secretary = _secretaryRepository.GetById(secretaryId);
            if (secretary.IsDeleted)
            {
                return null;
            }
            secretary.IsDeleted = true;
            await _secretaryRepository.UpdateAsync(secretary);
            return _mapper.Map<SecretaryDto>(secretary);
        }

        public async Task<IEnumerable<SecretaryDto>> ListSecretariesAsync()
        {
            var secretaries = await _secretaryRepository.ListAsync();
            var list = secretaries.ToList();
            List<SecretaryDto> Secretaries_List = new();

            for (int i = 0; i < secretaries.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    Secretaries_List.Add(_mapper.Map<SecretaryDto>(list[i]));
                }
            }

            return Secretaries_List;
        }

        public async Task<SecretaryDto> UpdateSecretaryAsync(SecretaryDto secretaryDto)
        {
            var secretary = _secretaryRepository.GetById(secretaryDto.IdS);
            if (!secretary.IsDeleted)
            {
                return null;
            }
            _mapper.Map(secretaryDto, secretary);
            await _secretaryRepository.UpdateAsync(secretary);
            return _mapper.Map<SecretaryDto>(secretary);
        }


    }
}
