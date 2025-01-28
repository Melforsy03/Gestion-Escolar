using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfStudSubCourse
{
    public class ProfStudSubCouseConsultResponseDto
    {
        public Dictionary<string, List<(string, string, int)>> EvaluationForCourse { get; set; }
    }
}
