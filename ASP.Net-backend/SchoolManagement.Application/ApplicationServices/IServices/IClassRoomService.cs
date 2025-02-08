using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IClassRoomService
    {
        Task<ClassRoomResponseDto> CreateClassRoomAsync(ClassRoomDto classRoomDto);
        Task<ClassRoomResponseDto> UpdateClassRoomAsync(ClassRoomResponseDto classRoomDto);
        Task<IEnumerable<ClassRoomResponseDto>> ListClassRoomAsync();
        Task<ClassRoomResponseDto> DeleteClassRoomByIdAsync(int classRoomDto);
        Task<ClassRoomMeanAmmount> GetClassRoomsMeanAmmount();
        Task<ClassRoomTechMeanAmmount> GetClassRoomTechAmmount();
    }
}
