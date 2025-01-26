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
namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ClassRoomMeanRequestService(Context context) : IClassRoomMeanRequestService
    {
      
        public async Task<ClassroomMeanRequestGetAviableResponseDto> GetAviableClassRoomMeanAsync(ClassRoomMeanRequestGetAviableDto classRoomMeanRequestDto)
        {   
            var User = context.Users.Where(u => u.UserName == classRoomMeanRequestDto.UserName).First();
            if (User == null || 
                !(Role.Professor == context.Roles.Where(r => r.Id == context.UserRoles.Where(ur => ur.UserId == User.Id).Select(p => p.RoleId).First()).Select(p => p.Name).First() ||
                Role.SuperAdmin == context.Roles.Where(r => r.Id == context.UserRoles.Where(ur => ur.UserId == User.Id).Select(p => p.RoleId).First()).Select(p => p.Name).First())) return null;
            
            var professor = context.Professors.Where(p => p.UserId == User.Id).First();
            var professorSubjects = context.ProfessorSubjects.Where(ps => ps.IdProf == professor.IdProf).ToList();
            ClassroomMeanRequestGetAviableResponseDto answer = new ClassroomMeanRequestGetAviableResponseDto();
            

            for (int i = 0; i < professorSubjects.Count(); i++)
            {
                var subject = context.Subjects.Find(professorSubjects[i].IdSub);
                var auxSub = context.SubjectAuxMeans.Where(p => p.IdSub == subject.IdSub).ToList();
                var auxMean = context.AuxiliaryMeans.Where(p => p.Subjects.Contains(subject));
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

        public async Task<ClassRoommeanRequestReserveResponseDto> ReserveClassRoomAndMeanAsync(ClassRoomMeanRequestReserveDto classRoomMeanRequestReserveDto)
        {
            var classRoom = context.ClassRooms.Where(cr => cr.Subjects.Contains(context.Subjects.Where(s => s.NameSub == classRoomMeanRequestReserveDto.subjectName).First())).First();
            if (!classRoom.IsAviable) return new ClassRoommeanRequestReserveResponseDto { success = false, message = "Fail!. ClassRoom " + classRoom.IdClassR + " is not aviable anymore." };
            classRoom.IsAviable = false;

            List<(List<AuxiliaryMeans>, int)> AuxMean = new List<(List<AuxiliaryMeans>, int)>(); 

            for(int i = 0; i < classRoomMeanRequestReserveDto.reserveMeans.Count(); i++)
            {
                AuxMean.Add((context.AuxiliaryMeans.Where(am => am.NameMean == classRoomMeanRequestReserveDto.reserveMeans[i].Item1 && am.isAviable).ToList(), classRoomMeanRequestReserveDto.reserveMeans[i].Item2));
                
                if (!(AuxMean[i].Item1.Count() < AuxMean[i].Item2)) return new ClassRoommeanRequestReserveResponseDto { success = false, message = "Fail!. Some AuxMean is not aviable anymore." };
                
                for(int j = 0; j < AuxMean[i].Item2; j++)
                {
                    AuxMean[i].Item1[j].isAviable = false;
                }
            }

            context.SaveChanges();

            return new ClassRoommeanRequestReserveResponseDto {success = true, classRoom = classRoom.IdClassR, message = "Success!", reserveMeans= classRoomMeanRequestReserveDto.reserveMeans, subjectName = classRoomMeanRequestReserveDto.subjectName };
        }
    }
}
