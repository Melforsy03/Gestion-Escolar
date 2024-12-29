using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class ClassRoomTechMeanDto
    {
        public int IdClassRoomTech { get; set; } = 0;
        public int IdClassRoom { get; set; } = 0;
        public int IdTechMean { get; set; } = 0;
    }
}
