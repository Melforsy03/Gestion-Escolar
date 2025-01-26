using SchoolManagement.Application.ApplicationServices.Maps_Dto.RequestDto.ClassRoomMeanRequest;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ResponseDto.ClassRoomMeanRequest;
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
        Task<ClassroomMeanRequestGetAviableResponseDto> GetAviableClassRoomMeanAsync(ClassRoomMeanRequestGetAviableDto classRoomMeanRequestService);
        Task<ClassRoommeanRequestReserveResponseDto> ReserveClassRoomAndMeanAsync(ClassRoomMeanRequestReserveDto classRoomMeanRequestDto);
    }
}
