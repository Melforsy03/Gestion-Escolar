using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Mean
    {
        public int IdMean { get; set; } = 0;
        public bool isAviable { get; set; }
        public bool isActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;
        public string NameMean {  get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int IdMaintenance { get; set; } = 0; 
        public List<Maintenance> maintenances { get; set; } = new List<Maintenance>();
    }
}
