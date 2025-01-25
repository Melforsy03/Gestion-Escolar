using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
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
namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ClassRoomMeanRequestService(Context context) : IClassRoomMeanRequestService
    {
      
        public async Task<(Dictionary<string, List<(string, int)>>, int[])> GetAviableClassRoomMeanAsync(ClassRoomMeanRequestDto classRoomMeanRequestDto)
        {   
            var User = context.Users.Where(u => u.UserName == classRoomMeanRequestDto.UserName).First();
            if (User == null || 
                !(Role.Professor == context.Roles.Where(r => r.Id == context.UserRoles.Where(ur => ur.UserId == User.Id).Select(p => p.RoleId).First()).Select(p => p.Name).First() ||
                Role.SuperAdmin == context.Roles.Where(r => r.Id == context.UserRoles.Where(ur => ur.UserId == User.Id).Select(p => p.RoleId).First()).Select(p => p.Name).First())) return (null, null) ;
            
            var professor = context.Professors.Where(p => p.UserId == User.Id).First();
            var professorSubjects = context.ProfessorSubjects.Where(ps => ps.IdProf == professor.IdProf).ToList();
            Dictionary<string, List<(string,int)>> SubjectsAndAuxiliaryMeans = new Dictionary<string,List<(string,int)>>();
            List<SubjectAuxMean> subjectAuxMeans = new List<SubjectAuxMean>();
                
            for(int i = 0; i < professorSubjects.Count; i++)
            {
               string SubjectName = context.Subjects.Where(s => s.IdSub == professorSubjects[i].IdSub).First().NameSub;
               List<(string, int)> AuxMeanAviable = new List<(string,int)>();
               List<SubjectAuxMean> subjectAuxMean = context.SubjectAuxMeans.Where(sam => sam.IdSub == professorSubjects[i].IdSub).ToList();
               List<AuxiliaryMeans> AuxMean = new List<AuxiliaryMeans>();
               for (int j = 0; j <  subjectAuxMean.Count(); j++)
               {
                   AuxMean = context.AuxiliaryMeans.Where(am => am.IdMean == subjectAuxMean[j].IdAuxMean).ToList();
               }

               for(int k = 0; k < AuxMean.Count(); k++)
               {
                  AuxMeanAviable.Add((AuxMean[k].NameMean, AuxMean[k].Aviable));     
               }

               SubjectsAndAuxiliaryMeans.Add(SubjectName, AuxMeanAviable);
                    
            }
            int[] classRoomsAviable = context.ClassRooms.Where(cr => cr.IsAviable).Select(p => p.IdClassR).ToArray();

            return (SubjectsAndAuxiliaryMeans, classRoomsAviable);
        }

        public async Task<(bool, string)> ReserveClassRoomAndMeanAsync(ClassRoomMeanRequestDto classRoomMeanRequestDto)
        {
            var classRoom = context.ClassRooms.Find(classRoomMeanRequestDto.ClassRoom);
            if (!classRoom.IsAviable) return (false, "ClassRoom is not longer aviable. Sorry!");
            classRoom.IsAviable = false;

            List<(AuxiliaryMeans, int)> AuxMean = new List<(AuxiliaryMeans, int)>(); 
            for(int i = 0; i < classRoomMeanRequestDto.AuxMean.Count(); i++)
            {
                AuxMean.Add((context.AuxiliaryMeans.Where(am => am.NameMean == classRoomMeanRequestDto.AuxMean[i].Item1).First(), classRoomMeanRequestDto.AuxMean[i].Item2));
                int newAviableAmmount = AuxMean[i].Item1.Aviable - AuxMean[i].Item2;
                if (newAviableAmmount < 0) return (false, "AuxiliaryMean: " + AuxMean[i].Item1.NameMean + "is not longer Aviable");
                AuxMean[i].Item1.Aviable = newAviableAmmount;
            }

            context.SaveChanges();

            return (true, "ClassRoom and AuxiliaryMeans reserverd succesfully");

        }
    }
}
