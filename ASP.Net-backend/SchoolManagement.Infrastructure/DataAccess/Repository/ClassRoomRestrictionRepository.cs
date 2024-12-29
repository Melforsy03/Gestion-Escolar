﻿using SchoolManagement.Domain.Entities;
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
    public class ClassRoomRestrictionRepository : GenericRepository<ClassRoomRestriction>, IClassRoomRestrictionRepository
    {
        public ClassRoomRestrictionRepository(Context context) : base(context) { }
    }
}
