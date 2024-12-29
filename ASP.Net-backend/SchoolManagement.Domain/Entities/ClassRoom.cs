using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class ClassRoom
    {
        public int IdClassR { get; set; } = 0;
        public bool IsAviable { get; set; } = true;
        public string Location { get; set; } = string.Empty;
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Restriction> Restrictions { get; set; } = new List<Restriction>();
        public List<TechnologicalMeans> TechnologicalMeans { get; set; } = new List<TechnologicalMeans>();

    }
}
