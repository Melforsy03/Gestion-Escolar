using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomMeanRequest
{
    public class ClassRoommeanRequestReserveResponseDto : ClassRoomMeanRequestReserveDto
    {
        public bool success { get; set; } = false;
        public string message { get; set; } = string.Empty;
        public int classRoom { get; set; }
    }
}
