using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class TechnologicalMeansDto
    {
        public int IdMean { get; set; } =0;
        public int isAviable { get; set; } = 0;
        public int Ammount { get; set; } = 0;
        public string NameMean { get; set; } = string.Empty;    
        public string State { get; set; } = string.Empty;
    }
}
