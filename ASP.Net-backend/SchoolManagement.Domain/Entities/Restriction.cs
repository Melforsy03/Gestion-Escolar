﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Restriction
    {
        public int IdRes { get; set; }
        public bool IsDeleted { get; set; }
        public string NameRes { get; set; }
        public List<ClassRoom> ClassRooms { get; set; } = new List<ClassRoom>();
    
    }
}
