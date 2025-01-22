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
        public bool isAviable { get; set; } = true;
        public string NameMean { get; set; } = string.Empty;    
        public string State { get; set; } = string.Empty;
    }
}
