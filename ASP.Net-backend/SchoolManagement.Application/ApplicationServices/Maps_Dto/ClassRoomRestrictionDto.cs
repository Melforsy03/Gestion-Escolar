using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class ClassRoomRestrictionDto
    {
        public int IdClassRoomRest { get; set; } = 0;
        public int IdClassRoom { get; set; } = 0;
        public int IdRest { get; set; } = 0;
    }
}
