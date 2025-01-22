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
    public class SecretaryProfessorStudentSubjectService : ISecretaryProfessorStudentSubjectService
    {
        private readonly ISecretaryProfessorStudentSubjectRepository _secretaryProfessorStudentSubjectRepository;
        private readonly ISecretaryRepository _secretaryRepository;
        private readonly IProfessorStudentSubjectRepository _professorStudentSubjectRepository;
        private readonly IMapper _mapper;

        public SecretaryProfessorStudentSubjectService(ISecretaryRepository secretaryRepository, ISecretaryProfessorStudentSubjectRepository secretaryProfessorStudentSubjectRepository, IProfessorStudentSubjectRepository professorStudentSubjectRepository, IMapper mapper)
        {
            _secretaryRepository = secretaryRepository;
            _secretaryProfessorStudentSubjectRepository = secretaryProfessorStudentSubjectRepository;
            _professorStudentSubjectRepository = professorStudentSubjectRepository;
            _mapper = mapper;
        }

        public async Task<SecretaryProfessorStudentSubjectDto> CreateSecretaryProfessorStudentSubjectAsync(SecretaryProfessorStudentSubjectDto secretaryProfessorStudentSubjectDto)
        {
            var secretaryProfessorStudentSubject = _mapper.Map<Domain.Relations.SecretaryProfessorStudentSubject>(secretaryProfessorStudentSubjectDto);
            secretaryProfessorStudentSubject.Secretary = await _secretaryRepository.GetByIdAsync(secretaryProfessorStudentSubject.IdSec);
            secretaryProfessorStudentSubject.Evaluation = await _professorStudentSubjectRepository.GetByIdAsync(secretaryProfessorStudentSubject.IdProfStudSub);
            var savedSecretaryProfessorStudentSubject = await _secretaryProfessorStudentSubjectRepository.CreateAsync(secretaryProfessorStudentSubject);
            return _mapper.Map<SecretaryProfessorStudentSubjectDto>(savedSecretaryProfessorStudentSubject);
        }

        public async Task DeleteSecretaryProfessorStudentSubjectByIdAsync(int id)
        {
            await _secretaryProfessorStudentSubjectRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<SecretaryProfessorStudentSubjectDto>> ListSecretariesProfessorStudentSubjectsAsync()
        {
            var secretaryProfessorStudentSubjects = await _secretaryProfessorStudentSubjectRepository.ListAsync();
            var list = secretaryProfessorStudentSubjects.ToList();
            List<SecretaryProfessorStudentSubjectDto> secretaryProfessorStudentSubjectList = new();

            for (int i = 0; i < list.Count; i++)
            {
                secretaryProfessorStudentSubjectList.Add(_mapper.Map<SecretaryProfessorStudentSubjectDto>(list[i]));
            }

            return secretaryProfessorStudentSubjectList;
        }

        public async Task<SecretaryProfessorStudentSubjectDto> UpdateSecretaryProfessorStudentSubjectAsync(SecretaryProfessorStudentSubjectDto secretaryProfessorStudentSubjectDto)
        {
            var secretaryProfessorStudentSubject = await _secretaryRepository.GetByIdAsync(secretaryProfessorStudentSubjectDto.IdSecProfStudSub);
            _mapper.Map(secretaryProfessorStudentSubjectDto, secretaryProfessorStudentSubject);
            await _secretaryRepository.UpdateAsync(secretaryProfessorStudentSubject);
            return _mapper.Map<SecretaryProfessorStudentSubjectDto>(secretaryProfessorStudentSubject);
        }


    }
}
