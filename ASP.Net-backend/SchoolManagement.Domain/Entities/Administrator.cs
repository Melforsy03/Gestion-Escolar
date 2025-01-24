using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Administrator
    {
        public int AdminId {  get; set; }
        public string UserId {  get; set; }
        public string AdminName {  get; set; }
        public int AdminSalary { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
