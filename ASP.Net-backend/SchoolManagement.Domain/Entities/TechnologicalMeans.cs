using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class TechnologicalMeans: Mean
    {
        public List<ClassRoom> ClassRooms { get; set; } = new List<ClassRoom>();
    }
}
