using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.Professor
{
    public class ProfessorsBadResponse
    {
        public string NameProf {  get; set; }
        public DateTime PunishmentDate { get; set; }
        public bool UseAuxMean {  get; set; }
        public int[] evals = new int[3];
    }
}
