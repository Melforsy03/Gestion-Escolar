using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomMeanRequest
{
    public class CMRGetAviableOrNotResponseDto
    {
        public Dictionary<string, Dictionary<string, int>> data { get; set; } = new Dictionary<string, Dictionary<string, int>>();

    }
}
