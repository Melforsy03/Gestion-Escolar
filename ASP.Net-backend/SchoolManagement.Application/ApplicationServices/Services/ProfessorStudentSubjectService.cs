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
            var professor = _context.Professors.Where(p => p.UserId == _context.Users.Where(u=>u.UserName == professorStudentSubjectDto.UserName).First().Id).First();
            var studentSubject = _context.StudentSubjects.Where(ss => ss.IdStud == professorStudentSubjectDto.IdStud && ss.IdStud == professorStudentSubjectDto.IdStud).First();            
            var savedProfessorStudentSubject = await _professorStudentSubjectRepository.CreateAsync(new ProfessorStudentSubject 
            {
                IdProf = professor.IdProf,
                IdStudSub = studentSubject.IdStudSub,
                Professor = professor,
                StudentSubject = studentSubject,
                StudentGrades = professorStudentSubjectDto.StudentGrades
            });

            return new ProfessorStudentSubjectResponseDto
            {
                IdProfStudSub = savedProfessorStudentSubject.IdProfStudSub,
                IdStud = savedProfessorStudentSubject.StudentSubject.Student.IdStud,
                IdSub = savedProfessorStudentSubject.StudentSubject.Subject.IdSub,
                professorName = professor.NameProf,
                studentName = _context.Students.Find(savedProfessorStudentSubject.StudentSubject.Student.IdStud).NameStud,
                subjectName =  _context.Subjects.Find(savedProfessorStudentSubject.StudentSubject.Subject.IdSub).NameSub
            };
        }

        public async Task<PSSResponseGetStudents> GetStudentsForSubjectAsync(int subjectId)
        {   
            var subject = _subjectRepository.GetById(subjectId);
            var students = _context.Students.Where(st => st.Subjects.Contains(subject)).ToList();
            if (students == null) return null;
            return new PSSResponseGetStudents { students = students };
        }

        public async Task<PSSResponseGetSubjects> GetSubjectsOfProfessorAsync(ProfessorStudentSubjectConsultDto professorStudentSubjectConsultDto)
        {   
            var professor = _context.Professors.Where(p => p.UserId == _context.Users.Where(u => u.UserName == professorStudentSubjectConsultDto.UserName).FirstOrDefault().Id).FirstOrDefault();
            if (professor == null) return null;
            var subjects = _context.Subjects.Where(s => s.Professors.Contains(professor)).ToList();
            
            return new PSSResponseGetSubjects { subjects = subjects};
        
        }


        public async Task<IEnumerable<ProfessorStudentSubjectResponseDto>> ListProfessorStudentSubjectAsync()
        {
            var list = _context.ProfessorStudentSubjects.ToList();
            
            List<ProfessorStudentSubjectResponseDto> professorStudentSubjects_List = new List<ProfessorStudentSubjectResponseDto>();
            for (int i = 0; i < list.Count(); i++)
            {
                professorStudentSubjects_List.Add(new ProfessorStudentSubjectResponseDto {
                    studentName = list[i].StudentSubject.Student.NameStud,
                    subjectName = list[i].StudentSubject.Subject.NameSub,
                    professorName = list[i].Professor.NameProf,
                    IdProfStudSub = list[i].IdProfStudSub,
                    IdStud = list[i].StudentSubject.IdStud,
                    IdSub = list[i].StudentSubject.IdSub,
                    StudentGrades = list[i].StudentGrades
                    
                });
                Console.WriteLine(list[i].StudentSubject.IdStud);
            }

            return professorStudentSubjects_List;
        }


        public async Task<IEnumerable<ProfessorStudentSubjectResponseDto>> ListProfessorStudentSubjectByUserNameAsync(ProfessorStudentSubjectConsultDto professorStudentSubjectConsultDto)
        {
            var professor =  _context.Professors.Where(p => p.UserId == _context.Users.Where(u => u.UserName == professorStudentSubjectConsultDto.UserName).First().Id).First();
            var professorStudentSubjects = _context.ProfessorStudentSubjects.Where(pss=> pss.IdProf == professor.IdProf);
            var list = professorStudentSubjects.ToList();
            List<ProfessorStudentSubjectResponseDto> professorStudentSubjects_List = new();
            for (int i = 0; i < professorStudentSubjects.Count(); i++)
            {
                professorStudentSubjects_List.Add(new ProfessorStudentSubjectResponseDto
                {
                    studentName = list[i].StudentSubject.Student.NameStud,
                    subjectName = list[i].StudentSubject.Subject.NameSub,
                    professorName = list[i].Professor.NameProf,
                    IdProfStudSub = list[i].IdProfStudSub,
                    IdStud = list[i].StudentSubject.Student.IdStud,
                    IdSub = list[i].StudentSubject.Subject.IdSub,
                    StudentGrades = list[i].StudentGrades

                });
            }

            return professorStudentSubjects_List;
        }
    }
}
