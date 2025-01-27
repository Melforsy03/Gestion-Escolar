using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorStudentSubject;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ProfessorStudentSubjectService : IProfessorStudentSubjectService
    {
        private readonly IProfessorStudentSubjectRepository _professorStudentSubjectRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly Context _context;
        private readonly IMapper _mapper;

        public ProfessorStudentSubjectService(Context context, ISubjectRepository subjectRepository, IProfessorStudentSubjectRepository professorStudentSubjectRepository, IProfessorRepository professorRepository, IStudentSubjectRepository studentSubjectRepository,IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _context = context;
            _professorStudentSubjectRepository = professorStudentSubjectRepository;
            _professorRepository = professorRepository;
            _studentSubjectRepository = studentSubjectRepository;
            _mapper = mapper;
        }

        public async Task<ProfessorStudentSubjectResponseDto> CreateProfessorStudentSubjectAsync(ProfessorStudentSubjectDto professorStudentSubjectDto)
        {
            var professorStudentSubject = _mapper.Map<ProfessorStudentSubject>(professorStudentSubjectDto);
            professorStudentSubject.Professor = _professorRepository.GetById(professorStudentSubjectDto.IdProf);
            professorStudentSubject.StudentSubject = _studentSubjectRepository.GetById(professorStudentSubjectDto.IdStudSub);            
            var savedProfessorStudentSubject = await _professorStudentSubjectRepository.CreateAsync(professorStudentSubject);
            return _mapper.Map<ProfessorStudentSubjectResponseDto>(savedProfessorStudentSubject);
        }

        public async Task<PSSResponseGetStudents> GetStudentsForSubjectAsync(int subjectId)
        {   
            var subject = _subjectRepository.GetById(subjectId);
            var students = _context.Students.Where(st => st.Subjects.Contains(subject)).ToList();
            if (students == null) return null;
            return new PSSResponseGetStudents { students = students };
        }

        public async Task<PSSResponseGetSubjects> GetSubjectsOfProfessorAsync(string userName)
        {   
            var professor = _context.Professors.Where(p => p.UserId == _context.Users.Where(u => u.UserName == userName).FirstOrDefault().Id).FirstOrDefault();
            if (professor == null) return null;
            var subjects = _context.Subjects.Where(s => s.Professors.Contains(professor)).ToList();
            
            return new PSSResponseGetSubjects { subjects = subjects};
        
        }

        public async Task<IEnumerable<ProfessorStudentSubjectResponseDto>> ListProfessorStudentSubjectAsync()
        {
            var professorStudentSubjects = await _professorStudentSubjectRepository.ListAsync();
            var list = professorStudentSubjects.ToList();
            List<ProfessorStudentSubjectResponseDto> professorStudentSubjects_List = new();
            for (int i = 0; i < professorStudentSubjects.Count(); i++)
            {
                professorStudentSubjects_List.Add(_mapper.Map<ProfessorStudentSubjectResponseDto>(list[i]));
            }

            return professorStudentSubjects_List;
        }

        public async Task<ProfessorStudentSubjectResponseDto> UpdateProfessorStudentSubjectAsync(ProfessorStudentSubjectResponseDto professorStudentSubjectDto)
        {
            var professorStudentSubject = await _professorStudentSubjectRepository.GetByIdAsync(professorStudentSubjectDto.IdProfStudSub);
            _mapper.Map(professorStudentSubjectDto, professorStudentSubject);
            await _professorStudentSubjectRepository.UpdateAsync(professorStudentSubject);
            return _mapper.Map<ProfessorStudentSubjectResponseDto>(professorStudentSubject);
        }
    }
}
