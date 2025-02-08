using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Notifications
{
    public class MeanNotifications
    {
        public int MeanNotId { get; set; }
        public string MeanName { get; set; }
        public int MeanID { get; set; }
        public bool BeenSended { get; set; }
        public string MeanType { get; set; }
    }
}
