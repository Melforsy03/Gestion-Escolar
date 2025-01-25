using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Services;
using SchoolManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IClassRoomMeanRequestService
    {
        Task<(Dictionary<string, List<(string, int)>>, int[])> GetAviableClassRoomMeanAsync(ClassRoomMeanRequestDto classRoomMeanRequestService);
        Task<(bool, string)> ReserveClassRoomAndMeanAsync(ClassRoomMeanRequestDto classRoomMeanRequestDto);
    }
}
