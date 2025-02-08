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
    public class ProfStudSubCourseRepository : GenericRepository<ProfStudSubCourse>, IProfStudSubCourseRepository
    {
        public ProfStudSubCourseRepository(Context context) : base(context) { }
    }
}
