using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class ClassRoomMeanRequestDto
    {
        public int ClassRoomId { get; set; }
        public List<string> MeanName { get; set; }
        public int[] MeanAmmount { get; set; }

    }
}
