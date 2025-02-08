using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Notifications
{
    public class ProfessorNotifications
    {
        public int ProfNotId { get; set; }
        public int IdProf { get; set; }
        public string ProfName { get; set; }
        public bool BeenSended { get; set; } = false;
        public Professor professor { get; set; }
    }
}
