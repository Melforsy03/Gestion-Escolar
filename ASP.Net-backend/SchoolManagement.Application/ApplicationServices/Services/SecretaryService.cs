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

        public async Task<(int Id, SecretaryDto secretary, string UserName, string Password)> CreateSecretaryAsync(SecretaryDto secretaryDto)
        {
            var secretary = _mapper.Map<Secretary>(secretaryDto);
            (User, string) User = await _trigger.RegisterUser(secretaryDto.NameS, "Secretary");
            secretary.UserId = User.Item1.Id;
            var savedSecretary = await _secretaryRepository.CreateAsync(secretary);

            secretaryDto = _mapper.Map<SecretaryDto>(savedSecretary);
            return (secretary.IdS, secretaryDto, User.Item1.UserName, User.Item2);
        }

        public async Task<(int Id, SecretaryDto secretary)> DeleteSecretaryByIdAsync(int secretaryId)
        {
            var secretary = _secretaryRepository.GetById(secretaryId);
            if (secretary.IsDeleted)
            {
                return (0,null);
            }
            secretary.IsDeleted = true;
            await _secretaryRepository.UpdateAsync(secretary);
            return (secretary.IdS, _mapper.Map<SecretaryDto>(secretary));
        }

        public async Task<IEnumerable<(int Id, SecretaryDto secretary)>> ListSecretariesAsync()
        {
            var secretaries = await _secretaryRepository.ListAsync();
            var list = secretaries.ToList();
            List<(int Id, SecretaryDto secretary)> Secretaries_List = new List<(int Id, SecretaryDto secretary)>();

            for (int i = 0; i < secretaries.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    Secretaries_List.Add((list[i].IdS, _mapper.Map<SecretaryDto>(list[i])));
                }
            }

            return Secretaries_List;
        }

        public async Task<(int Id, SecretaryDto secretary)> UpdateSecretaryAsync((int Id, SecretaryDto secretaryDto) secretaryInfo)
        {
            var secretary = _secretaryRepository.GetById(secretaryInfo.Id);
            if (!secretary.IsDeleted)
            {
                return (0, null);
            }
            _mapper.Map(secretaryInfo.secretaryDto, secretary);
            await _secretaryRepository.UpdateAsync(secretary);
            return (secretary.IdS, _mapper.Map<SecretaryDto>(secretary));
        }


    }
}
