using SchoolManagement.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Common.GenericInterface;

namespace SchoolManagement.Infrastructure.DataAccess.IRepository
{
    public interface IProfessorRepository: IRepository<Professor>
    {

    }
}
