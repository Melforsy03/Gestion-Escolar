using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Common.Implementation;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.DataAccess.Repository
{
    public class SecretaryRepository : GenericRepository<Secretary>, ISecretaryRepository
    {
        public SecretaryRepository(Context context) : base(context) { }
    }
}
