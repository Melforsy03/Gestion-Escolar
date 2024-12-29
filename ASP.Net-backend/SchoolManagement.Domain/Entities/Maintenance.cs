using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Maintenance
    {
        public int IdM { get; set; } = 0;
        public DateOnly MaintenanceDate { get; set; } = new DateOnly();
        public int Cost { get; set; } = 0;
        public int typeOfMean { get; set; } = 0;
        public int IdAuxMean { get; set; } = 0;
        public AuxiliaryMeans auxMean { get; set; } = new AuxiliaryMeans();
        public int IdTechMean { get; set; } = 0;
        public TechnologicalMeans technologicalMean { get; set;} = new TechnologicalMeans();

    }
}
