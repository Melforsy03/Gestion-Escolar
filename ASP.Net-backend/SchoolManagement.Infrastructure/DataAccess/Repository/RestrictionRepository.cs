﻿using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Common.Implementation;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.DataAccess.Repository
{
    public class RestrictionRepository : GenericRepository<Restriction>, IRestrictionRepository
    {
        public RestrictionRepository(Context context) : base(context) { }
    }
}
