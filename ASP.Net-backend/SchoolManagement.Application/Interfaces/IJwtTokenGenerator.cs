using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Infrastructure.Identity;

namespace SchoolManagement.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(User user, string userRole);
    }
}
