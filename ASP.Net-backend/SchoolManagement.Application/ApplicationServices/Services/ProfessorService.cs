using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Professor;
using SchoolManagement.Application.Common;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure;
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
        private readonly Context _context;
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public ProfessorService(IProfessorRepository professorRepository, IMapper mapper, Triggers trigger, Context context)
        {
            _professorRepository = professorRepository;
            _context = context;
            _mapper = mapper;
            _trigger = trigger;
        }

        public async Task<ProfessorsEvaluations> GetGoodProfessors()
        {
            var professors = _context.Professors.ToList();
            ProfessorsEvaluations profEval = new ProfessorsEvaluations();
            profEval.professorsAndSubjects = new Dictionary<string, List<string>>();
            foreach(var prof in professors)
            {
                var evaluation = _context.ProfStudSubCourses.Where(pssc => pssc.IdProf == prof.IdProf).Select(p => p.Evaluation).ToList();
                if(_trigger.GetAverage(evaluation) > 8)
                {
                    var profSubjects = _context.ProfessorSubjects.Where(ps => ps.IdProf == prof.IdProf);
                    List<string> subjects = new List<string>();
                    foreach(var ps in profSubjects)
                    {
                        subjects.Add(_context.Subjects.Where(s => s.IdSub == ps.IdSub).First().NameSub);
                    }
                    profEval.professorsAndSubjects.Add(prof.NameProf, subjects);
                }
            }

            return profEval;

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
            if (professor.IsDeleted)
            {
                return null;
            }
            if(professor.Salary > professorInfo.professor.Salary) _context.ProfessorsPunishments.Add(new ProfessorPunishment {IdProf = professor.IdProf, Professor = professor, PunishmentDate = DateTime.Now });

            _mapper.Map(professorInfo.professor, professor);
            await _professorRepository.UpdateAsync(professor);
            ProfessorResponseDto answer = new ProfessorResponseDto();
            answer.Id = professor.IdProf;
            answer.professor = _mapper.Map<ProfessorDto>(professor);
            _context.SaveChanges();
            return answer;

        }
    }
}