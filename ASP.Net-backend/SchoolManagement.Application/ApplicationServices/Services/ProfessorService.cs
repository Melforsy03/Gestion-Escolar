﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public async Task<ProfessorDto> CreateProfessorAsync(ProfessorDto professorDto)
        {
            var professor = _mapper.Map<Professor>(professorDto);
            User User = await _trigger.RegisterUser(professorDto.NameProf, "Professor");
            professor.UserId = User.Id;
            var savedProfessor = await _professorRepository.CreateAsync(professor);

            professorDto = _mapper.Map<ProfessorDto>(savedProfessor);
            professorDto.UserName = User.UserName;
            professorDto.PasswordHash = User.PasswordHash;
            return professorDto;
            
        }

        public async Task<ProfessorDto> DeleteProfessorByIdAsync(int professorId)
        {
            var professor =  _professorRepository.GetById(professorId);
            if (professor.IsDeleted)
            {
                return null;
            }
            professor.IsDeleted = true;
            await _professorRepository.UpdateAsync(professor);
            return _mapper.Map<ProfessorDto>(professor);
        }

        public async Task<IEnumerable<ProfessorDto>> ListProfessorAsync()
        {
            var professors = await _professorRepository.ListAsync();
            var list = professors.ToList();
            List<ProfessorDto> Professors_List = new();
            for (int i = 0; i < professors.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    Professors_List.Add(_mapper.Map<ProfessorDto>(list[i]));
                }
                
            }

            return Professors_List;
        }

        public async Task<ProfessorDto> UpdateProfessorAsync(ProfessorDto professorDto)
        {
            var professor = _professorRepository.GetById(professorDto.IdProf);
            if (!professor.IsDeleted)
            {
                return null;
            }
            _mapper.Map(professorDto, professor);
            await _professorRepository.UpdateAsync(professor);
            return _mapper.Map<ProfessorDto>(professor);

        }
    }
}