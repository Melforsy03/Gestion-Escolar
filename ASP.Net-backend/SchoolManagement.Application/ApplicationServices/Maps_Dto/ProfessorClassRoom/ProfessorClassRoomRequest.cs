using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorClassRoom
{
    public class ProfessorClassRoomRequest
    {
        public string NameProf { get; set; }
        public string Spec { get; set; }
        public Dictionary <int , Dictionary<string, string>> ClassRoomsAndMeans { get; set; }
    }
}
