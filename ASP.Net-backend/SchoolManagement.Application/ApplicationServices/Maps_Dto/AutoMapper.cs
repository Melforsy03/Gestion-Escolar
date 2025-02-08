using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Infrastructure.Identity;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Administrator;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Professor;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Secretary;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Student;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Course;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.SubjectAuxMean;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.AuxiliaryMeans;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoom;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomRestriction;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomTechMean;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Maintenance;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Restriction;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Subject;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorStudentSubject;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorSubject;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.StudentSubject;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.TechnologicalMeans;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfStudSubCourse;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.SecretaryProfessorStudentSubject;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //Mapeo de Usuarios y Entidades Relacionadas
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<LoginDto, User>();

            CreateMap<AdministratorDto, Domain.Entities.Administrator>();
            CreateMap< Domain.Entities.Administrator, AdministratorDto >();

            CreateMap<ProfessorDto, Domain.Entities.Professor>();
            CreateMap<Domain.Entities.Professor, ProfessorDto>();


            CreateMap<SecretaryDto, Domain.Entities.Secretary>();
            CreateMap<Domain.Entities.Secretary, SecretaryDto>();

            CreateMap<StudentDto, Domain.Entities.Student>();
            CreateMap<Domain.Entities.Student, StudentDto>();



            //Entradas y salidas de Entidades(optimizacion)



            //Curso
            CreateMap<CourseDto, Domain.Entities.Course>();
            CreateMap<Domain.Entities.Course, CourseDto>();

            CreateMap<CourseResponseDto, Domain.Entities.Course>();
            CreateMap<Domain.Entities.Course, CourseResponseDto>();

            //Aula
            CreateMap<ClassRoomDto, Domain.Entities.ClassRoom>();
            CreateMap<Domain.Entities.ClassRoom, ClassRoomDto>();

            CreateMap<Domain.Entities.ClassRoom, ClassRoomResponseDto>();
            CreateMap<ClassRoomResponseDto, Domain.Entities.ClassRoom>();

            //Restricciones del aula

            CreateMap<ClassRoomRestrictionDto, Domain.Relations.ClassRoomRestriction>();
            CreateMap<Domain.Relations.ClassRoomRestriction, ClassRoomRestrictionDto>();

            CreateMap<ClassRoomTechMeanDto, Domain.Relations.ClassRoomTechMean>();
            CreateMap<Domain.Relations.ClassRoomTechMean, ClassRoomTechMeanDto>();

            //Mantenimiento
            CreateMap<MaintenanceDto, Domain.Entities.Maintenance>();
            CreateMap<Domain.Entities.Maintenance, MaintenanceDto>();

            CreateMap<MaintenanceResponseDto, Domain.Entities.Maintenance>();
            CreateMap<Domain.Entities.Maintenance, MaintenanceResponseDto>();

            //Medios Auxiliares
            CreateMap<AuxiliaryMeansDto, Domain.Entities.AuxiliaryMeans>();
            CreateMap<Domain.Entities.AuxiliaryMeans, AuxiliaryMeansDto>();

            CreateMap<AuxiliaryMeansResponseDto, Domain.Entities.AuxiliaryMeans>();
            CreateMap<Domain.Entities.AuxiliaryMeans, AuxiliaryMeansResponseDto>();
            
            //Restricciones
            CreateMap<RestrictionDto, Domain.Entities.Restriction>();
            CreateMap<Domain.Entities.Restriction, RestrictionDto>();

            CreateMap<RestrictionResponseDto, Domain.Entities.Restriction>();
            CreateMap<Domain.Entities.Restriction, RestrictionResponseDto>();

            //Asignaturas
            CreateMap<SubjectDto, Domain.Entities.Subject>();
            CreateMap<Domain.Entities.Subject, SubjectDto>();

            CreateMap<SubjectResponseDto, Domain.Entities.Subject>();
            CreateMap<Domain.Entities.Subject, SubjectResponseDto>();

            //Notas
            CreateMap<ProfessorStudentSubjectDto, Domain.Relations.ProfessorStudentSubject>();
            CreateMap<Domain.Relations.ProfessorStudentSubject, ProfessorStudentSubjectDto>();

            CreateMap<ProfessorStudentSubjectResponseDto, Domain.Relations.ProfessorStudentSubject>();
            CreateMap<Domain.Relations.ProfessorStudentSubject, ProfessorStudentSubjectResponseDto>();

            //Estudiante en Asignatura
            CreateMap<StudentSubjectResponseDto, Domain.Relations.StudentSubject>();
            CreateMap<Domain.Relations.StudentSubject, StudentSubjectResponseDto>();

            CreateMap<StudentSubjectDto, Domain.Relations.StudentSubject>();
            CreateMap<Domain.Relations.StudentSubject, StudentSubjectDto>();


            //asignaturas del profesor
            CreateMap<ProfessorSubjectDto, Domain.Relations.ProfessorSubject>();
            CreateMap<Domain.Relations.ProfessorSubject, ProfessorSubjectDto>();

            CreateMap<ProfessorSubjectResponseDto, Domain.Relations.ProfessorSubject>();
            CreateMap<Domain.Relations.ProfessorSubject, ProfessorSubjectResponseDto>();


            //medios tecnologicos
            CreateMap<TechnologicalMeansResponseDto, Domain.Entities.TechnologicalMeans>();
            CreateMap<Domain.Entities.TechnologicalMeans, TechnologicalMeansResponseDto>();

            CreateMap<TechnologicalMeansDto, Domain.Entities.TechnologicalMeans>();
            CreateMap<Domain.Entities.TechnologicalMeans, TechnologicalMeansDto>();


            //Asignatura y medios auxiliares

            CreateMap<SubjectAuxMeanDto, Domain.Relations.SubjectAuxMean>();
            CreateMap<Domain.Relations.SubjectAuxMean, SubjectAuxMeanResponseDto>();

            CreateMap<SubjectAuxMeanResponseDto, Domain.Relations.SubjectAuxMean>();
            CreateMap< Domain.Relations.SubjectAuxMean, SubjectAuxMeanResponseDto >();

            //Secretario asignando Notas
            CreateMap<SecretaryProfessorStudentSubjectDto, Domain.Relations.SecretaryProfessorStudentSubject>();
            CreateMap<Domain.Relations.SecretaryProfessorStudentSubject, SecretaryProfessorStudentSubjectDto>();

            CreateMap<SecretaryProfessorStudentSubjectResponseDto, Domain.Relations.SecretaryProfessorStudentSubject>();
            CreateMap<Domain.Relations.SecretaryProfessorStudentSubject, SecretaryProfessorStudentSubjectResponseDto>();


            //relacion de profesor con asignatura en curso
            CreateMap<ProfStudSubCourseResponseDto, Domain.Relations.ProfStudSubCourse>();
            CreateMap<Domain.Relations.ProfStudSubCourse, ProfStudSubCourseResponseDto>();

            CreateMap<ProfStudSubCourseDto,Domain.Relations.ProfStudSubCourse>();
            CreateMap<Domain.Relations.ProfStudSubCourse, ProfStudSubCourseDto>();
        
            //Aula y medios tecnologicos
            CreateMap<ClassRoomTechMeanResponseDto, ClassRoomTechMeanResponseDto>();
            CreateMap<Domain.Relations.ClassRoomTechMean, ClassRoomTechMeanResponseDto>();
            CreateMap<ClassRoomTechMeanResponseDto, Domain.Relations.ClassRoomTechMean>();
            CreateMap<ClassRoomTechMeanDto, ClassRoomTechMeanResponseDto>();
            CreateMap<ClassRoomTechMeanResponseDto, ClassRoomTechMeanDto>();
        }
    }
}
