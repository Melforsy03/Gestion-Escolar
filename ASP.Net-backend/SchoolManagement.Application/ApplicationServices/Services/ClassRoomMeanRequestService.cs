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
                var subject = _context.Subjects.Find(professorSubjects[i].IdSub);
                var auxSub = _context.SubjectAuxMeans.Where(p => p.IdSub == subject.IdSub).ToList();
                var auxMean = _context.AuxiliaryMeans.Where(p => p.Subjects.Contains(subject));
                var auxMeanAndAmmount = auxMean.Where(am => am.isAviable).GroupBy(am => am.NameMean).Select(g => new { Name = g.Key, Ammount = g.Count()}).ToList();
                List<(string, int)> list = new List<(string, int)> ();
                for(int j = 0; j <  auxMeanAndAmmount.Count(); j++)
                {
                    list.Add((auxMeanAndAmmount[j].Name, auxMeanAndAmmount[j].Ammount));                   
                }
                answer.data.Add(subject.NameSub, list);
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
            var subjects = _context.Subjects.Where(s => professorSubjects.Select(ps => ps.IdSub).ToList().Contains(s.IdSub) && !_context.ClassRooms.Where(c => c.Subjects.Where(sa => sa.IdSub == s.IdSub) != null).First().IsAviable).ToList();
            
            CMRGetAviableOrNotResponseDto answer = new CMRGetAviableOrNotResponseDto();

            for(int i = 0; i < subjects.Count(); i++)
            {
                var auxSub = _context.SubjectAuxMeans.Where(p => p.IdSub == subjects[i].IdSub).ToList();
                var auxMean = _context.AuxiliaryMeans.Where(p => p.Subjects.Contains(subjects[i]));
                var auxMeanAndAmmmount = auxMean.Where(am => !(am.isAviable)).GroupBy(am => am.NameMean).Select(g => new { Name = g.Key, Ammount = g.Count() }).ToList();

                List<(string, int)> list = new List<(string, int)>();

                for(int j = 0; j < auxMeanAndAmmmount.Count(); j++)
                {
                    list.Add((auxMeanAndAmmmount[j].Name, auxMeanAndAmmmount[j].Ammount));
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

            List<(List<AuxiliaryMeans>, int)> AuxMean = new List<(List<AuxiliaryMeans>, int)>();

            for (int i = 0; i < classRoomMeanRequestReserveDto.reserveMeans.Count(); i++)
            {
                if (classRoomMeanRequestReserveDto.reserve)
                {
                    AuxMean.Add((_context.AuxiliaryMeans.Where(am => am.NameMean == classRoomMeanRequestReserveDto.reserveMeans[i].Item1 && am.isAviable).ToList(), classRoomMeanRequestReserveDto.reserveMeans[i].Item2));
                }
                else
                {
                    AuxMean.Add((_context.AuxiliaryMeans.Where(am => am.NameMean == classRoomMeanRequestReserveDto.reserveMeans[i].Item1 && !am.isAviable).ToList(), classRoomMeanRequestReserveDto.reserveMeans[i].Item2));
                }
                if (!(AuxMean[i].Item1.Count() < AuxMean[i].Item2)) return new ClassRoommeanRequestReserveResponseDto { success = false, message = "Fail!. Some AuxMean is not aviable anymore." };

                for (int j = 0; j < AuxMean[i].Item2; j++)
                {
                    if (classRoomMeanRequestReserveDto.reserve)
                    {
                        AuxMean[i].Item1[j].isAviable = false;
                    }
                    else
                    {

                        AuxMean[i].Item1[j].isAviable = true;
                    }
                }
            }

            _context.SaveChanges();

            return new ClassRoommeanRequestReserveResponseDto { success = true, classRoom = classRoom.IdClassR, message = "Success!", reserveMeans = classRoomMeanRequestReserveDto.reserveMeans, subjectName = classRoomMeanRequestReserveDto.subjectName };
        }
      
    }
}
