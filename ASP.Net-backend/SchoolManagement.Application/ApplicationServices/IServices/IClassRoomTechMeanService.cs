using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomTechMean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IClassRoomTechMeanService
    {
        Task<ClassRoomTechMeanResponseDto> CreateClassRoomTechMeanAsync(ClassRoomTechMeanDto classRoomTechMeanDto);
        Task<ClassRoomTechMeanResponseDto> UpdateClassRoomTechMeanAsync(ClassRoomTechMeanResponseDto classRoomTechMeanDto);
        Task<IEnumerable<ClassRoomTechMeanResponseDto>> ListClassRoomTechMeansAsync();
        Task<ClassRoomTechMeanResponseDto> DeleteClassRoomTechMeanByIdAsync(int id);
    }

}
