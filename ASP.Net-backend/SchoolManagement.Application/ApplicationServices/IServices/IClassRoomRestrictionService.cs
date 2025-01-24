using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IClassRoomRestrictionService
    {
        Task<ClassRoomRestrictionDto> CreateClassRoomRestrictionAsync(ClassRoomRestrictionDto classRoomRestrictionDto);
        Task<ClassRoomRestrictionDto> UpdateClassRoomRestrictionAsync(ClassRoomRestrictionDto classRoomRestrictionDto);
        Task<IEnumerable<ClassRoomRestrictionDto>> ListClassRoomRestrictionsAsync();
        Task<ClassRoomRestrictionDto> DeleteClassRoomRestrictionByIdAsync(int id);
    }

}
