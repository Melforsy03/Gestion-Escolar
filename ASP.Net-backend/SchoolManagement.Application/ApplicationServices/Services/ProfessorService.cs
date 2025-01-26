using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ResponseDto.Professor;
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
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public ProfessorService(IProfessorRepository professorRepository, IMapper mapper, Triggers trigger)
        {
            _professorRepository = professorRepository;
            _mapper = mapper;
            _trigger = trigger;
        }

        public async Task<ProfessorCreateResponseDto> CreateProfessorAsync(ProfessorDto professorDto)
        {
            var professor = _mapper.Map<Professor>(professorDto);
            (User,string) User = await _trigger.RegisterUser(professorDto.NameProf, "Professor");
            professor.UserId = User.Item1.Id;

            var savedProfessor = await _professorRepository.CreateAsync(professor);

            professorDto = _mapper.Map<ProfessorDto>(savedProfessor);
            ProfessorCreateResponseDto answer = new ProfessorCreateResponseDto();
            answer.Id = professor.IdProf;
            answer.professor = professorDto;
            answer.UserName = User.Item1.UserName;
            answer.Password =User.Item2;
            return answer;
            
        }

        public async Task<ProfessorResponseDto> DeleteProfessorByIdAsync(int professorId)
        {
            var professor =  _professorRepository.GetById(professorId);
            if (professor.IsDeleted)
            {
                return (null);
            }
            professor.IsDeleted = true;
            await _professorRepository.UpdateAsync(professor);
            ProfessorResponseDto answer = new ProfessorResponseDto();
            answer.Id = professor.IdProf;
            answer.professor = _mapper.Map<ProfessorDto>(professor);

            return answer;
        }

        public async Task<IEnumerable<ProfessorResponseDto>> ListProfessorAsync()
        {
            var professors = await _professorRepository.ListAsync();
            var list = professors.ToList();
            List<ProfessorResponseDto> Professors_List = new List<ProfessorResponseDto>();
            for (int i = 0; i < professors.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {   
                    ProfessorResponseDto professorResponse = new ProfessorResponseDto();
                    professorResponse.Id = list[i].IdProf;
                    professorResponse.professor = _mapper.Map<ProfessorDto>(list[i]);
                    Professors_List.Add(professorResponse);
                }
                
            }

            return Professors_List;
        }

        public async Task<ProfessorResponseDto> UpdateProfessorAsync(ProfessorResponseDto professorInfo)
        {
            var professor = _professorRepository.GetById(professorInfo.Id);
            if (!professor.IsDeleted)
            {
                return null;
            }
            _mapper.Map(professorInfo.professor, professor);
            await _professorRepository.UpdateAsync(professor);
            ProfessorResponseDto answer = new ProfessorResponseDto();
            answer.Id = professor.IdProf;
            answer.professor = _mapper.Map<ProfessorDto>(professor);
            return answer;

        }
    }
}