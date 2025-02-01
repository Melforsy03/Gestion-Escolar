using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoom
{
    public class ClassRoomMeanAmmount
    {
        public Dictionary<int, Dictionary<string, string>> ClassRoomsAndMeans { get; set; }
        public int AmmountOfMaintenance2yo { get; set; }
    }
}
