using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Role;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomMeanRequest;
using AutoMapper;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ClassRoomMeanRequestService : IClassRoomMeanRequestService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;

        public ClassRoomMeanRequestService(IMapper mapper, Context context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CMRGetAviableOrNotResponseDto> GetAviableClassRoomMeanAsync(ClassRoomMeanRequestGetAviableDto classRoomMeanRequestDto)
        {   
            var User = _context.Users.Where(u => u.UserName == classRoomMeanRequestDto.UserName).First();
            if (User == null || 
                !(Role.Professor == _context.Roles.Where(r => r.Id == _context.UserRoles.Where(ur => ur.UserId == User.Id).Select(p => p.RoleId).First()).Select(p => p.Name).First() ||
                Role.SuperAdmin == _context.Roles.Where(r => r.Id == _context.UserRoles.Where(ur => ur.UserId == User.Id).Select(p => p.RoleId).First()).Select(p => p.Name).First())) return null;
            
            var professor = _context.Professors.Where(p => p.UserId == User.Id).First();
            var professorSubjects = _context.ProfessorSubjects.Where(ps => ps.IdProf == professor.IdProf).ToList();
            
            CMRGetAviableOrNotResponseDto answer = new CMRGetAviableOrNotResponseDto();
            

            for (int i = 0; i < professorSubjects.Count(); i++)
            {
                var subject = _context.Subjects.Where(s => s.IdSub == professorSubjects[i].IdSub).First();
                var auxSub = _context.SubjectAuxMeans.Where(p => p.IdSub == subject.IdSub).ToList();
                var auxMean = new List<AuxiliaryMeans>();
                for(int j = 0; j < auxSub.Count(); j++)
                {
                    auxMean.Add(_context.AuxiliaryMeans.Where(a => a.IdMean == auxSub[j].IdAuxMean).First());
                }

                var auxMeanAndAmmount = auxMean.Where(am => am.isAviable).GroupBy(am => am.NameMean).Select(g => new { Name = g.Key, Ammount = g.Count()}).ToList();
                Dictionary<string, int> list = new Dictionary<string, int>();

                for(int j = 0; j <  auxMeanAndAmmount.Count(); j++)
                {
                    (string, int) item = (auxMeanAndAmmount[j].Name, auxMeanAndAmmount[j].Ammount);
                    Console.WriteLine(auxMeanAndAmmount[j].Name + "lala " + auxMeanAndAmmount[j].Ammount);
                    Console.WriteLine(item.Item1 + " " + item.Item2);
                    list.Add(item.Item1, item.Item2);
                }
                Console.WriteLine(list.Count);
                answer.data.Add(subject.NameSub, list );
            }

            return answer;
        }

        public async Task<CMRGetAviableOrNotResponseDto>GetNotAviableClassRoomMeanAsync(ClassRoomMeanRequestGetAviableDto classRoomMeanRequestDto)
        {
            var User = _context.Users.Where(u => u.UserName == classRoomMeanRequestDto.UserName).First();
            if(User == null || !(Role.Professor == _context.Roles.Where(r => r.Id == _context.UserRoles.Where(ur => ur.UserId == User.Id).Select(p => p.RoleId).First()).Select(p => p.Name).First() ||
                Role.SuperAdmin == _context.Roles.Where(r => r.Id == _context.UserRoles.Where(ur => ur.UserId == User.Id).Select(p => p.RoleId).First()).Select(p => p.Name).First())) return null;
            
            var professor = _context.Professors.Where(p => p.UserId == User.Id).First();
            var professorSubjects = _context.ProfessorSubjects.Where(ps => ps.IdProf == professor.IdProf).ToList();
            List<Subject> subjects = new List<Subject>();
            for(int i = 0; i < professorSubjects.Count(); i++)
            {
                subjects.AddRange(_context.Subjects.Where(s => s.IdSub == professorSubjects[i].IdSub).ToList());
            }
            List<Subject> subjectsWithAviableClassRoom = new List<Subject>();
            for(int i = 0; i < subjects.Count(); i++)
            {
                if (subjects[i].classRoom.IsAviable)
                {
                    subjectsWithAviableClassRoom.Add(subjects[i]);
                }
            }
            
            CMRGetAviableOrNotResponseDto answer = new CMRGetAviableOrNotResponseDto();

            for(int i = 0; i < subjectsWithAviableClassRoom.Count(); i++)
            {
                var auxSub = _context.SubjectAuxMeans.Where(p => p.IdSub == subjectsWithAviableClassRoom[i].IdSub).ToList();
                var auxMean = _context.AuxiliaryMeans.Where(p => p.Subjects.Contains(subjectsWithAviableClassRoom[i]));
                var auxMeanAndAmmmount = auxMean.Where(am => !(am.isAviable)).GroupBy(am => am.NameMean).Select(g => new { Name = g.Key, Ammount = g.Count() }).ToList();

                Dictionary<string, int> list = new Dictionary<string, int>();

                for(int j = 0; j < auxMeanAndAmmmount.Count(); j++)
                {
                    list.Add(auxMeanAndAmmmount[j].Name, auxMeanAndAmmmount[j].Ammount);
                }
                answer.data.Add(subjects[i].NameSub, list);
            }

            return answer;
        }

        public async Task<ClassRoommeanRequestReserveResponseDto> ReserveClassRoomAndMeanAsync(ClassRoomMeanRequestReserveDto classRoomMeanRequestReserveDto)
        {
            var classRoom = _context.ClassRooms.Where(cr => cr.Subjects.Contains(_context.Subjects.Where(s => s.NameSub == classRoomMeanRequestReserveDto.subjectName).First())).First();
            if (classRoomMeanRequestReserveDto.reserve)
            {
                if (!classRoom.IsAviable) return new ClassRoommeanRequestReserveResponseDto { success = false, message = "Fail!. ClassRoom " + classRoom.IdClassR + " is not aviable anymore." };
                classRoom.IsAviable = false;
            }
            else
            {
                if (classRoom.IsAviable) return new ClassRoommeanRequestReserveResponseDto { success = false, message = "Fail!. ClassRoom " + classRoom.IdClassR + " is aviable already." };
                classRoom.IsAviable = true;
            }

            var User = _context.Users.Where(u => u.UserName == classRoomMeanRequestReserveDto.userName).FirstOrDefault();
            var professor = _context.Professors.Where(p => p.UserId == User.Id).First();

            if (classRoomMeanRequestReserveDto.reserve)
            {
                var list = _context.ProfessorClassRooms.Where(p => p.IdClassR == classRoom.IdClassR && p.IdProf == professor.IdProf).ToList();
                if(list.Count() == 0)
                {
                    _context.ProfessorClassRooms.Add(new ProfessorClassRoom { IdProf = professor.IdProf, IdClassR = classRoom.IdClassR, professor = professor, classRoom = classRoom });
                }
            }

            Dictionary<List<AuxiliaryMeans>, int> AuxMean = new Dictionary<List<AuxiliaryMeans>, int>();

            int i = 0;
            foreach(var rm in classRoomMeanRequestReserveDto.reserveMeans)
            {
                if (classRoomMeanRequestReserveDto.reserve)
                {
                    AuxMean.Add(_context.AuxiliaryMeans.Where(am => am.NameMean == rm.Key && am.isAviable).ToList(), rm.Value);
                }
                else
                {
                    AuxMean.Add(_context.AuxiliaryMeans.Where(am => am.NameMean == rm.Key && !am.isAviable).ToList(), rm.Value);
                }

                foreach(var am in AuxMean)
                {
                    if (am.Key.Count() < am.Value) return new ClassRoommeanRequestReserveResponseDto { success = false, message = "Fail!. Some AuxMean is not aviable anymore." };

                    for (int j = 0; j < am.Value; j++)
                    {
                        if (classRoomMeanRequestReserveDto.reserve)
                        {
                            am.Key[j].isAviable = false;
                        }
                        else
                        {

                            am.Key[j].isAviable = true;
                        }
                    }
                }
               
                
            }

            _context.SaveChanges();

            return new ClassRoommeanRequestReserveResponseDto { success = true, classRoom = classRoom.IdClassR, message = "Success!", reserveMeans = classRoomMeanRequestReserveDto.reserveMeans, subjectName = classRoomMeanRequestReserveDto.subjectName };
        }
      
    }
}
