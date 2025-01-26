using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.Maintenance
{
    public class MaintenanceDto
    {
        public DateOnly MaintenanceDate { get; set; } = new DateOnly();
        public int Cost { get; set; } = 0;
        public int IdMean { get; set; } = 0;
        public int typeOfMean { get; set; } = 0;
        public int IdAuxMean { get; set; } = 0;
        public int IdTechMean { get; set; } = 0;
    }
}
