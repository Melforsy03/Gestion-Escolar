using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Infrastructure.Common.Implementation;
using SchoolManagement.Infrastructure.DataAccess.IRepository;

namespace SchoolManagement.Infrastructure.DataAccess.Repository
{
    public class ProfessorRepository: GenericRepository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(Context context):base(context) { }
    }
}
