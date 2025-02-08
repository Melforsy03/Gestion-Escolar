using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomRestriction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IClassRoomRestrictionService
    {
        Task<ClassRoomRestrictionResponseDto> CreateClassRoomRestrictionAsync(ClassRoomRestrictionDto classRoomRestrictionDto);
        Task<ClassRoomRestrictionResponseDto> UpdateClassRoomRestrictionAsync(ClassRoomRestrictionResponseDto classRoomRestrictionDto);
        Task<IEnumerable<ClassRoomRestrictionResponseDto>> ListClassRoomRestrictionsAsync();
        Task<ClassRoomRestrictionResponseDto> DeleteClassRoomRestrictionByIdAsync(int id);
    }

}
