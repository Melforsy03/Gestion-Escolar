using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;

        public ProfessorService(IProfessorRepository professorRepository, IMapper mapper)
        {
            _professorRepository = professorRepository;
            _mapper = mapper;
        }

        public async Task<ProfessorDto> CreateProfessorAsync(User professorUser)
        {
            Professor professor = new();
            professor.NameProf = professorUser.UserName!;
            professor.userId = professorUser.Id;
            professor.Email = professorUser.Email!;

            await _professorRepository.CreateAsync(professor);
            return _mapper.Map<professor>(professor);
        }

        public async Task DeleteProfessorByIdAsync(int professorDto)
        {
            await _professorRepository.DeleteByIdAsync(professorDto);
        }

        public async Task<IEnumerable<ProfessorDto>> ListProfessorAsync()
        {
            var professors = await _professorRepository.ListAsync();
            var list = professors.ToList();
            List<ProfessorDto> Professors_List = new();
            for (int i = 0; i < professors.Count(); i++)
            {
                Professors_List.Add(_mapper.Map<ProfessorDto>(list[i]));
            }

            return Professors_List;
        }

        public async Task<ProfessorDto> UpdateProfessorAsync(ProfessorDto professorDto)
        {
            var professor = _professorRepository.GetById(professorDto.IdProf);
            _mapper.Map(professorDto, professor);
            await _professorRepository.UpdateAsync(professor);
            return _mapper.Map<ProfessorDto>(professor);
        }
    }
}