using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomTechMean
{
    public class ClassRoomTechMeanResponseDto: ClassRoomTechMeanDto
    {
        public int IdClassRoomTech { get; set; } = 0;
        public string TechName { get; set; }
    }
}
