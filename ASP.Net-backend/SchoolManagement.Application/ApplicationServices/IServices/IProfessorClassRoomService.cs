using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorClassRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IProfessorClassRoomService
    {
        Task<ProfessorClassRoomGetSpecRequest> GetSpecs();

        Task<IEnumerable<ProfessorClassRoomRequest>> GetAllProfessorsBySpec(string spec);
    }
}
