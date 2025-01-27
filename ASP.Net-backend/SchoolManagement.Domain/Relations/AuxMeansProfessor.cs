using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Relations
{
    public class AuxMeansProfessor
    {
        public int IdAuxMeanProf {  get; set; } = 0;
        public int IdAuxMean { get; set; } = 0;
        public int IdProf { get; set; } = 0;
        public Professor Professors { get; set; }
        public AuxiliaryMeans AuxiliaryMean { get; set; }
    }
}
