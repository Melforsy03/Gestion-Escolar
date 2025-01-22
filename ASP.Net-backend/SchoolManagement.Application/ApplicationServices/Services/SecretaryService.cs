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
    public class SecretaryService : ISecretaryService
    {
        private readonly ISecretaryRepository _secretaryRepository;
        private readonly IMapper _mapper;

        public SecretaryService(ISecretaryRepository secretaryRepository, IMapper mapper)
        {
            _secretaryRepository = secretaryRepository;
            _mapper = mapper;
        }

        public async Task<SecretaryDto> CreateSecretaryAsync(SecretaryDto secretaryDto)
        {
            var secretary = _mapper.Map<Domain.Entities.Secretary>(secretaryDto);
            var savedSecretary = await _secretaryRepository.CreateAsync(secretary);
            return _mapper.Map<SecretaryDto>(savedSecretary);
        }

        public async Task DeleteSecretaryByIdAsync(int secretaryId)
        {
            await _secretaryRepository.DeleteByIdAsync(secretaryId);
        }

        public async Task<IEnumerable<SecretaryDto>> ListSecretariesAsync()
        {
            var secretaries = await _secretaryRepository.ListAsync();
            var list = secretaries.ToList();
            List<SecretaryDto> secretaryList = new();
            for (int i = 0; i < secretaries.Count(); i++)
            {
                secretaryList.Add(_mapper.Map<SecretaryDto>(list[i]));
            }

            return secretaryList;
        }

        public async Task<SecretaryDto> UpdateSecretaryAsync(SecretaryDto secretaryDto)
        {
            var secretary = _secretaryRepository.GetById(secretaryDto.IdS); 
            _mapper.Map(secretaryDto, secretary);
            await _secretaryRepository.UpdateAsync(secretary);
            return _mapper.Map<SecretaryDto>(secretary);
        }

    }
}
