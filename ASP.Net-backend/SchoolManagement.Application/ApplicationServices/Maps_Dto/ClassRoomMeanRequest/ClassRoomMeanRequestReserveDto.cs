using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomMeanRequest
{
    public class ClassRoomMeanRequestReserveDto
    {   
        public string userName {  get; set; }
        public string subjectName { get; set; }
        public List<(string MeanName, int Ammount)> reserveMeans { get; set; }
        public bool reserve { get; set; }
    }
}
