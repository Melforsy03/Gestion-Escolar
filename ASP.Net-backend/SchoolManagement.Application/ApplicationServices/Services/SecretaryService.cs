using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Secretary;
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

        public async Task<SecretaryCreateResponseDto> CreateSecretaryAsync(SecretaryDto secretaryDto)
        {
            var secretary = _mapper.Map<Secretary>(secretaryDto);
            (User, string) User = await _trigger.RegisterUser(secretaryDto.NameS, "Secretary");
            secretary.UserId = User.Item1.Id;
            var savedSecretary = await _secretaryRepository.CreateAsync(secretary);
            SecretaryCreateResponseDto answer = new SecretaryCreateResponseDto();
            answer.Id = secretary.IdS;
            answer.secretary = _mapper.Map<SecretaryDto>(savedSecretary);
            answer.UserName = User.Item1.UserName;
            answer.Password = User.Item2;
            return answer;
        }

        public async Task<SecretaryResponseDto> DeleteSecretaryByIdAsync(int secretaryId)
        {
            var secretary = _secretaryRepository.GetById(secretaryId);
            if (secretary.IsDeleted)
            {
                return null;
            }
            secretary.IsDeleted = true;
            await _secretaryRepository.UpdateAsync(secretary);
            SecretaryResponseDto answer = new SecretaryResponseDto();
            answer.Id = secretary.IdS;
            answer.secretary = _mapper.Map<SecretaryDto>(secretary);
            return answer;
        }

        public async Task<IEnumerable<SecretaryResponseDto>> ListSecretariesAsync()
        {
            var secretaries = await _secretaryRepository.ListAsync();
            var list = secretaries.ToList();
            List<SecretaryResponseDto> Secretaries_List = new List<SecretaryResponseDto>();

            for (int i = 0; i < secretaries.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    SecretaryResponseDto answer = new SecretaryResponseDto();
                    answer.Id = list[i].IdS;
                    answer.secretary = _mapper.Map<SecretaryDto>(list[i]);
                    Secretaries_List.Add(answer);
                }
            }

            return Secretaries_List;
        }

        public async Task<SecretaryResponseDto> UpdateSecretaryAsync(SecretaryResponseDto secretaryInfo)
        {
            var secretary = _secretaryRepository.GetById(secretaryInfo.Id);
            if (secretary.IsDeleted)
            {
                return null;
            }
            _mapper.Map(secretaryInfo.secretary, secretary);
            await _secretaryRepository.UpdateAsync(secretary);
            SecretaryResponseDto answer = new SecretaryResponseDto();
            answer.Id = secretary.IdS;
            answer.secretary = _mapper.Map<SecretaryDto>(secretary);
            return answer;
        }


    }
}
