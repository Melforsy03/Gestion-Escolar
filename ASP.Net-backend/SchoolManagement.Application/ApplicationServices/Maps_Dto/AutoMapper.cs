﻿using System;
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
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<LoginDto, User>();

            CreateMap<ProfessorDto, Domain.Entities.Professor>();
            CreateMap<Domain.Entities.Professor, ProfessorDto>();

            CreateMap<SubjectDto, Domain.Entities.Subject>();
            CreateMap<Domain.Entities.Subject, SubjectDto>();

            CreateMap<RestrictionDto, Domain.Entities.Restriction>();
            CreateMap<Domain.Entities.Restriction, RestrictionDto>();

            CreateMap<SecretaryDto, Domain.Entities.Secretary>();
            CreateMap<Domain.Entities.Secretary, SecretaryDto>();

            CreateMap<CourseDto, Domain.Entities.Course>();
            CreateMap<Domain.Entities.Course, CourseDto>();

            CreateMap<MaintenanceDto, Domain.Entities.Maintenance>();
            CreateMap<Domain.Entities.Maintenance, MaintenanceDto>();

            CreateMap<AuxiliaryMeansDto, Domain.Entities.AuxiliaryMeans>();
            CreateMap<Domain.Entities.AuxiliaryMeans, AuxiliaryMeansDto>();

            CreateMap<TechnologicalMeansDto, Domain.Entities.TechnologicalMeans>();
            CreateMap<Domain.Entities.TechnologicalMeans, TechnologicalMeansDto>();

            CreateMap<ClassRoomDto, Domain.Entities.ClassRoom>();
            CreateMap<Domain.Entities.ClassRoom, ClassRoomDto>();

            CreateMap<StudentDto, Domain.Entities.Student>();
            CreateMap<Domain.Entities.Student, StudentDto>();
        }
    }
}
