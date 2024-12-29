﻿using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Relations
{
    public class ClassRoomTechMean
    {
        public int IdClassRoomTech {  get; set; } = 0;
        public int IdClassRoom { get; set; } = 0;
        public ClassRoom ClassRoom { get; set; } = new ClassRoom();
        public int IdTechMean { get; set; } = 0;
        public TechnologicalMeans TechnologicalMeans { get; set; } = new TechnologicalMeans(); 
    }
}
