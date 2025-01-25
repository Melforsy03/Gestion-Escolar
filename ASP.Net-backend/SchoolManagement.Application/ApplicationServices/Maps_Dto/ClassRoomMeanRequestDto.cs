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
        public string UserName { get; set; }
        public int ClassRoom {  get; set; }
        public List<(string, int)> AuxMean { get; set; }

    }
}
