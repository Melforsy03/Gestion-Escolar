﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.Student
{
    public class StudentCreateResponseDto
    {
        public int Id { get; set; }
        public int IdC {  get; set; }
        public StudentDto student { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
