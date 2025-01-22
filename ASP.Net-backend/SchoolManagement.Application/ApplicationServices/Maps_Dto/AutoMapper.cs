using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Infrastructure.Identity;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<LoginDto, User>();

            CreateMap<ProfessorDto, Professor>();
            CreateMap<Professor, ProfessorDto>();

            CreateMap<SubjectDto, Subject>();
            CreateMap<Subject, SubjectDto>();

            CreateMap<RestrictionDto, Restriction>();
            CreateMap<Restriction, RestrictionDto>();

            CreateMap<SecretaryDto, Secretary>();
            CreateMap<Secretary, SecretaryDto>();

            CreateMap<SecretaryProfessorStudentSubjectDto, SecretaryProfessorStudentSubject>();
            CreateMap<SecretaryProfessorStudentSubject, SecretaryProfessorStudentSubjectDto>();

            CreateMap<CourseDto, Course>();
            CreateMap<Course, CourseDto>();

            CreateMap<MaintenanceDto, Maintenance>();
            CreateMap<Maintenance, MaintenanceDto>();

            CreateMap<AuxiliaryMeansDto, AuxiliaryMeans>();
            CreateMap<AuxiliaryMeans, AuxiliaryMeansDto>();

            CreateMap<TechnologicalMeansDto, TechnologicalMeans>();
            CreateMap<TechnologicalMeans, TechnologicalMeansDto>();

            CreateMap<ClassRoomDto, ClassRoom>();
            CreateMap<ClassRoom, ClassRoomDto>();

            CreateMap<ClassRoomRestrictionDto, ClassRoomRestriction>();
            CreateMap<ClassRoomRestriction, ClassRoomRestrictionDto>();

            CreateMap<ClassRoomTechMeanDto, ClassRoomTechMean>();
            CreateMap<ClassRoomTechMean, ClassRoomTechMeanDto>();

            CreateMap<StudentDto, Student>();
            CreateMap<Student, StudentDto>();

            CreateMap<StudentSubjectDto, StudentSubject>();
            CreateMap<StudentSubject, StudentSubjectDto>();

            CreateMap<ProfessorSubjectDto,  ProfessorSubject>();
            CreateMap<ProfessorSubject, ProfessorSubjectDto>(); 

            CreateMap<ProfessorStudentSubjectDto, ProfessorStudentSubject>();
            CreateMap<ProfessorStudentSubject, ProfessorStudentSubjectDto>();

            CreateMap<ProfStudSubCourseDto, ProfStudSubCourse>();
            CreateMap<ProfStudSubCourse, ProfStudSubCourseDto>();
        }
    }
}
