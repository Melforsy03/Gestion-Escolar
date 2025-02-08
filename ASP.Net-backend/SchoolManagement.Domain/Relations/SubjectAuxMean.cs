using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Relations
{
    public class SubjectAuxMean
    {
        public int IdSubAuxMean { get; set; } = 0;
        public int IdSub { get; set; } = 0;
        public Subject Subject { get; set; } = new Subject();
        public int IdAuxMean = 0;
        public AuxiliaryMeans AuxMean { get; set; } = new AuxiliaryMeans();  
    }
}
