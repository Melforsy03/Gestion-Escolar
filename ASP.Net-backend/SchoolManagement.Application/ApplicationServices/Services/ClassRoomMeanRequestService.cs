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
    public class ClassRoomMeanRequestService(Context DbContext) : IClassRoomMeanRequestService
    {
      
        public async Task<ClassRoomMeanRequestDto> GetAviableClassRoomMean(ClassRoomMeanRequestDto classRoomMeanRequestDto, Context context)
        {   
            var ClassRoom = await DbContext.ClassRooms.FindAsync(classRoomMeanRequestDto.ClassRoomId);
            
            if (ClassRoom != null) 
            {
                if(ClassRoom.IsAviable)
                {
                    ClassRoom.IsAviable = false;
                }

                var professor = context.Professors.Where(p => p.UserId == classRoomMeanRequestDto.UserId).ToList();
                var professorSubjects = context.ProfessorSubjects.Where(ps => ps.IdProf == professor[0].IdProf).ToList();
                List<SubjectAuxMean> subjectAuxMeans = new List<SubjectAuxMean>();
                
                for(int i = 0; i < professorSubjects.Count; i++)
                {
                    subjectAuxMeans.AddRange((context.SubjectAuxMeans.Where(sa => sa.IdSub == professorSubjects[i].IdSub).ToList()));
                }

                List<AuxiliaryMeans> auxiliaryMeans = new List<AuxiliaryMeans>();

                
            }
            else
            {
                throw new NotImplementedException();
            }

            List<Mean>[] MeansRequested = new List<Mean>[classRoomMeanRequestDto.MeanName.Count];
         
           throw new NotImplementedException();
        }
    }
}
