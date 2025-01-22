using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ClassRoomMeanRequestService(Context DbContext) : IClassRoomMeanRequestService
    {
        public async Task<ClassRoomMeanRequestDto> GetAviableClassRoomMean(ClassRoomMeanRequestDto classRoomMeanRequestDto)
        {
            var ClassRoom = await DbContext.ClassRooms.FindAsync(classRoomMeanRequestDto.ClassRoomId);
            
            if (ClassRoom != null) 
            {
                if(ClassRoom.IsAviable)
                {
                    ClassRoom.IsAviable = false;
                }
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
