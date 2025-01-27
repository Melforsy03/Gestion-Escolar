using SchoolManagement.Domain.Relations;
using SchoolManagement.Infrastructure.Common.Implementation;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.DataAccess.Repository
{
    public class AuxMeansProfessorRepository : GenericRepository<AuxMeansProfessor>, IAuxMeansProfessorRepository
    {
        public AuxMeansProfessorRepository(Context context) : base(context) { }
    }
}
