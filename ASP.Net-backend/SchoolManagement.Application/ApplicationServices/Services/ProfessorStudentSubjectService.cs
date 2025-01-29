using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorStudentSubject;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Domain.Role;
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
            var user = _context.Users.Where(u => u.UserName == professorStudentSubjectDto.UserName).FirstOrDefault();
            string role = _context.Roles.Where(r => r.Id == _context.UserRoles.Where(ur => ur.UserId == user.Id).FirstOrDefault().RoleId).FirstOrDefault().Name;
            if (role == Role.Professor || role == Role.SuperAdmin)
            {
                var professor = _context.Professors.Where(p => p.UserId == _context.Users.Where(u => u.UserName == professorStudentSubjectDto.UserName).First().Id).First();
                var studentSubject = _context.StudentSubjects.Where(ss => ss.IdStud == professorStudentSubjectDto.IdStud && ss.IdStud == professorStudentSubjectDto.IdStud).First();
                var profStudSub = new ProfessorStudentSubject
                {
                    IdProf = professor.IdProf,
                    IdStudSub = studentSubject.IdStudSub,
                    Professor = professor,
                    StudentSubject = studentSubject,
                    StudentGrades = professorStudentSubjectDto.StudentGrades
                };
                _context.ProfessorStudentSubjects.Add(profStudSub);
                _context.SaveChanges();


                return new ProfessorStudentSubjectResponseDto
                {
                    IdProfStudSub = profStudSub.IdProfStudSub,
                    IdStud = studentSubject.IdStud,
                    IdSub = studentSubject.IdSub,
                    studentName = _context.Students.Where(s => s.IdStud == studentSubject.IdStud).First().NameStud,
                    subjectName = _context.Subjects.Where(s => s.IdSub == studentSubject.IdSub).First().NameSub,
                    professorName = professor.NameProf,
                    StudentGrades = professorStudentSubjectDto.StudentGrades

                };
            }
            else if (role == Role.Secretary)
            {
                var professor = _context.Professors.Where(p => p.IdProf == _context.ProfessorSubjects.Where(ps => ps.IdSub == professorStudentSubjectDto.IdSub).First().IdProf).FirstOrDefault();
                var studentSubject = _context.StudentSubjects.Where(ss => ss.IdStud == professorStudentSubjectDto.IdStud && ss.IdStud == professorStudentSubjectDto.IdStud).First();
                var profStudSub = new ProfessorStudentSubject
                {
                    IdProf = professor.IdProf,
                    IdStudSub = studentSubject.IdStudSub,
                    Professor = professor,
                    StudentSubject = studentSubject,
                    StudentGrades = professorStudentSubjectDto.StudentGrades
                };
                _context.ProfessorStudentSubjects.Add(profStudSub);
                _context.SaveChanges();

                var secretary = _context.Secretaries.Where(s => s.UserId == user.Id).First();
                return new ProfessorStudentSubjectResponseDto
                {
                    IdProfStudSub = profStudSub.IdProfStudSub,
                    IdStud = studentSubject.IdStud,
                    IdSub = studentSubject.IdSub,
                    studentName = _context.Students.Where(s => s.IdStud == studentSubject.IdStud).First().NameStud,
                    subjectName = _context.Subjects.Where(s => s.IdSub == studentSubject.IdSub).First().NameSub,
                    professorName = secretary.NameS,
                    StudentGrades = professorStudentSubjectDto.StudentGrades

                };
            }


            throw new NotImplementedException();
        }

        public async Task<PSSResponseGetStudents> GetStudentsForSubjectAsync(int subjectId)
        {   
            var subject = _subjectRepository.GetById(subjectId);
            var students = _context.Students.Where(st => st.Subjects.Contains(subject)).ToList();
            if (students == null) return null;
            return new PSSResponseGetStudents { students = students };
        }

        public async Task<PSSResponseGetSubjects> GetSubjectsOfProfessorAsync(string UserName)
        {   
            var professor = _context.Professors.Where(p => p.UserId == _context.Users.Where(u => u.UserName == UserName).FirstOrDefault().Id).FirstOrDefault();
            if (professor == null) return null;
            var subjects = _context.Subjects.Where(s => s.Professors.Contains(professor)).ToList();
            
            return new PSSResponseGetSubjects { subjects = subjects};
        
        }

        public Task<PSSResponseGetSubjects> GetSubjectsOfProfessorAsync(ProfessorStudentSubjectConsultDto professorStudentSubjectConsultDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProfessorStudentSubjectResponseDto>> ListProfessorStudentSubjectAsync()
        {
            var list = _context.ProfessorStudentSubjects.ToList();
            
            List<ProfessorStudentSubjectResponseDto> professorStudentSubjects_List = new List<ProfessorStudentSubjectResponseDto>();
            for (int i = 0; i < list.Count(); i++)
            {

                var studentSubject = _context.StudentSubjects.Where(ss => ss.IdStudSub == list[i].IdStudSub).First();

                professorStudentSubjects_List.Add(new ProfessorStudentSubjectResponseDto
                {
                    studentName = _context.Students.Where(s => s.IdStud == studentSubject.IdStud).First().NameStud,
                    subjectName = _context.Subjects.Where(s => s.IdSub == studentSubject.IdSub).First().NameSub,
                    professorName = _context.Professors.Where(p => p.IdProf == list[i].IdProf).First().NameProf,
                    IdProfStudSub = list[i].IdProfStudSub,
                    IdStud = studentSubject.IdStud,
                    IdSub = studentSubject.IdSub,
                    StudentGrades = list[i].StudentGrades

                });
            }

            return professorStudentSubjects_List;
        }

        public async Task<bool> UpdateProfessorStudentSubjectAsync(int profStudSubID, float studentGrade)
        {
            var profStudSub = await _context.ProfessorStudentSubjects.FindAsync(profStudSubID);
            profStudSub.StudentGrades = studentGrade;
            _context.SaveChanges();

            return true;
        }


        public async Task<IEnumerable<ProfessorStudentSubjectResponseDto>> ListProfessorStudentSubjectByUserNameAsync(string UserName)
        {
            var professor =  _context.Professors.Where(p => p.UserId == _context.Users.Where(u => u.UserName == UserName).First().Id).First();
            var professorStudentSubjects = _context.ProfessorStudentSubjects.Where(pss=> pss.IdProf == professor.IdProf);
            var list = professorStudentSubjects.ToList();
            List<ProfessorStudentSubjectResponseDto> professorStudentSubjects_List = new();
            for (int i = 0; i < professorStudentSubjects.Count(); i++)
            {
                var studentSubject = _context.StudentSubjects.Where(ss=>ss.IdStudSub == list[i].IdStudSub).First();

               professorStudentSubjects_List.Add(new ProfessorStudentSubjectResponseDto
                {
                    studentName = _context.Students.Where(s=>s.IdStud == studentSubject.IdStud).First().NameStud,
                    subjectName = _context.Subjects.Where(s => s.IdSub == studentSubject.IdSub).First().NameSub,
                    professorName = professor.NameProf,
                    IdProfStudSub = list[i].IdProfStudSub,
                    IdStud = studentSubject.IdStud,
                    IdSub = studentSubject.IdSub,
                    StudentGrades = list[i].StudentGrades

                });
            }

            return professorStudentSubjects_List;
        }

       
    }
}
