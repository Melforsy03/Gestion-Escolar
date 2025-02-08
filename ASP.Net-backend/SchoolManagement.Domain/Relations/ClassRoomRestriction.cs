using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Relations
{
    public class ClassRoomRestriction
    {
        public int IdClassRoomRest {  get; set; } = 0;
        public int IdClassRoom { get; set;} = 0;
        public ClassRoom ClassRoom { get; set; } = new ClassRoom();
        public int IdRest { get; set; } = 0;
        public Restriction Restriction { get; set; } = new Restriction();
    }
}
