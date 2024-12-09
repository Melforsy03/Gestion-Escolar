using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class MaintenanceDto
    {
        public int IdM { get; set; }
        public DateOnly MaintenanceDate { get; set; }
        public int Cost { get; set; }
    }
}
