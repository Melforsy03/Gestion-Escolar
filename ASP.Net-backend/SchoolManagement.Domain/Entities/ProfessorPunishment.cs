using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class ProfessorPunishment
    {
        public int IdPun {  get; set; }
        public int IdProf { get; set; }
        public Professor Professor { get; set; }
        public DateTime PunishmentDate { get; set; }
    }
}
