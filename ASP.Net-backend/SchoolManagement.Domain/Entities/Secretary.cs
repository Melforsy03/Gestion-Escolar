using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Secretary
    {
        public int IdS { get; set; }
        public string NameS { get; set; }
        public string LastNameS { get; set; }
        public int SalaryS { get; set; }
        public bool IsDeleted { get; set; }
    }
}
