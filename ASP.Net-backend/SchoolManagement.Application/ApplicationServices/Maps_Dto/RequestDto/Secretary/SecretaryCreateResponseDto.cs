using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ResponseDto.Secretary
{
    public class SecretaryCreateResponseDto
    {
        public int Id {  get; set; }
        public SecretaryDto secretary { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
