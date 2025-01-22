using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class AuxiliaryMeans:Mean
    {
        public string Type { get; set; } = string.Empty;
        public List<Subject> Subjects { get; set; } = new List<Subject> { };
    }
}
