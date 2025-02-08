﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Course
    {
        public int IdC { get; set; }
        public string CourseName {  get; set; }
        public bool IsDeleted { get; set; }
        public List<Student> Students { get; set; }
    }
}
