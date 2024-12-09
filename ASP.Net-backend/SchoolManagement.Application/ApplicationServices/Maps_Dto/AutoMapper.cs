using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ProfessorDto, Domain.Entities.Professor>();
            CreateMap<Domain.Entities.Professor, ProfessorDto>();
        }
    }
}
