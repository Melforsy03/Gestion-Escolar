using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IClassRoomService
    {
        Task<ClassRoomDto> CreateClassRoomAsync(ClassRoomDto classRoomDto);
        Task<ClassRoomDto> UpdateClassRoomAsync(ClassRoomDto classRoomDto);
        Task<IEnumerable<ClassRoomDto>> ListClassRoomAsync();
        Task DeleteClassRoomByIdAsync(int classRoomDto);
    }
}
