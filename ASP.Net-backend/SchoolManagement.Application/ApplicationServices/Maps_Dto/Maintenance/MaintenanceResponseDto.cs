﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.Maintenance
{
    public class MaintenanceResponseDto: MaintenanceDto
    {
        public int IdM { get; set; } = 0;
        public string meanName { get; set; }
    }
}
