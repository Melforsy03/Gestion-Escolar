using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Role;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomMeanRequest;
using AutoMapper;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ClassRoomMeanRequestService : IClassRoomMeanRequestService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;

        public ClassRoomMeanRequestService(IMapper mapper, Context context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CMRGetAviableOrNotResponseDto> GetAviableClassRoomMeanAsync(ClassRoomMeanRequestGetAviableDto classRoomMeanRequestDto)
        {
            // Obtiene el usuario basado en el nombre de usuario proporcionado en el DTO
            var User = _context.Users.Where(u => u.UserName == classRoomMeanRequestDto.UserName).First();

            // Verifica si el usuario existe y si tiene el rol de Profesor o SuperAdmin
            if (User == null ||
                !(Role.Professor == _context.Roles.Where(r => r.Id == _context.UserRoles.Where(ur => ur.UserId == User.Id).Select(p => p.RoleId).First()).Select(p => p.Name).First() ||
                Role.SuperAdmin == _context.Roles.Where(r => r.Id == _context.UserRoles.Where(ur => ur.UserId == User.Id).Select(p => p.RoleId).First()).Select(p => p.Name).First()))
            {
                return null; // Retorna null si el usuario no es válido
            }

            // Obtiene el profesor asociado al usuario
            var professor = _context.Professors.Where(p => p.UserId == User.Id).First();

            // Obtiene las materias que imparte el profesor
            var professorSubjects = _context.ProfessorSubjects.Where(ps => ps.IdProf == professor.IdProf).ToList();

            // Inicializa la respuesta que se devolverá
            CMRGetAviableOrNotResponseDto answer = new CMRGetAviableOrNotResponseDto();

            // Itera sobre las materias del profesor para obtener los medios auxiliares disponibles
            for (int i = 0; i < professorSubjects.Count(); i++)
            {
                var subject = _context.Subjects.Where(s => s.IdSub == professorSubjects[i].IdSub).First();

                // Obtiene los medios auxiliares asociados a la materia
                var auxSub = _context.SubjectAuxMeans.Where(p => p.IdSub == subject.IdSub).ToList();
                var auxMean = new List<AuxiliaryMeans>();

                // Carga los medios auxiliares en una lista
                for (int j = 0; j < auxSub.Count(); j++)
                {
                    auxMean.Add(_context.AuxiliaryMeans.Where(a => a.IdMean == auxSub[j].IdAuxMean).First());
                }

                // Agrupa los medios auxiliares disponibles por su nombre y cuenta cuántos hay de cada uno
                var auxMeanAndAmmount = auxMean.Where(am => am.isAviable)
                                                .GroupBy(am => am.NameMean)
                                                .Select(g => new { Name = g.Key, Ammount = g.Count() })
                                                .ToList();

                Dictionary<string, int> list = new Dictionary<string, int>();

                // Agrega los resultados al diccionario de respuesta
                for (int j = 0; j < auxMeanAndAmmount.Count(); j++)
                {
                    (string, int) item = (auxMeanAndAmmount[j].Name, auxMeanAndAmmount[j].Ammount);
                    list.Add(item.Item1, item.Item2);
                }

                Console.WriteLine(list.Count);

                // Agrega la información de la materia y sus medios auxiliares disponibles a la respuesta
                answer.data.Add(subject.NameSub, list);
            }

            return answer; // Retorna la respuesta final
        }

        public async Task<CMRGetAviableOrNotResponseDto> GetNotAviableClassRoomMeanAsync(ClassRoomMeanRequestGetAviableDto classRoomMeanRequestDto)
        {
            // Obtiene el usuario basado en el nombre de usuario proporcionado en el DTO
            var User = _context.Users.Where(u => u.UserName == classRoomMeanRequestDto.UserName).First();

            // Verifica si el usuario existe y si tiene el rol de Profesor o SuperAdmin
            if (User == null || !(Role.Professor == _context.Roles.Where(r => r.Id == _context.UserRoles.Where(ur => ur.UserId == User.Id).Select(p => p.RoleId).First()).Select(p => p.Name).First() ||
                Role.SuperAdmin == _context.Roles.Where(r => r.Id == _context.UserRoles.Where(ur => ur.UserId == User.Id).Select(p => p.RoleId).First()).Select(p => p.Name).First()))
            {
                return null; // Retorna null si el usuario no es válido
            }

            // Obtiene el profesor asociado al usuario
            var professor = _context.Professors.Where(p => p.UserId == User.Id).First();

            // Obtiene las materias que imparte el profesor
            var professorSubjects = _context.ProfessorSubjects.Where(ps => ps.IdProf == professor.IdProf).ToList();

            List<Subject> subjects = new List<Subject>();

            // Carga las materias en una lista
            for (int i = 0; i < professorSubjects.Count(); i++)
            {
                subjects.AddRange(_context.Subjects.Where(s => s.IdSub == professorSubjects[i].IdSub).ToList());
            }

            List<Subject> subjectsWithAviableClassRoom = new List<Subject>();

            // Filtra las materias que tienen aulas disponibles
            for (int i = 0; i < subjects.Count(); i++)
            {
                if (subjects[i].classRoom.IsAviable)
                {
                    subjectsWithAviableClassRoom.Add(subjects[i]);
                }
            }

            CMRGetAviableOrNotResponseDto answer = new CMRGetAviableOrNotResponseDto();

            // Itera sobre las materias con aulas disponibles para obtener los medios auxiliares no disponibles
            for (int i = 0; i < subjectsWithAviableClassRoom.Count(); i++)
            {
                var auxSub = _context.SubjectAuxMeans.Where(p => p.IdSub == subjectsWithAviableClassRoom[i].IdSub).ToList();

                // Obtiene los medios auxiliares asociados a la materia actual
                var auxMean = _context.AuxiliaryMeans.Where(p => p.Subjects.Contains(subjectsWithAviableClassRoom[i]));

                // Agrupa los medios auxiliares no disponibles por su nombre y cuenta cuántos hay de cada uno
                var auxMeanAndAmmmount = auxMean.Where(am => !(am.isAviable))
                                                 .GroupBy(am => am.NameMean)
                                                 .Select(g => new { Name = g.Key, Ammount = g.Count() })
                                                 .ToList();

                Dictionary<string, int> list = new Dictionary<string, int>();

                // Agrega los resultados al diccionario de respuesta
                for (int j = 0; j < auxMeanAndAmmmount.Count(); j++)
                {
                    list.Add(auxMeanAndAmmmount[j].Name, auxMeanAndAmmmount[j].Ammount);
                }

                answer.data.Add(subjects[i].NameSub, list); // Agrega la información de la materia y sus medios auxiliares no disponibles a la respuesta
            }

            return answer; // Retorna la respuesta final
        }

        public async Task<ClassRoommeanRequestReserveResponseDto> ReserveClassRoomAndMeanAsync(ClassRoomMeanRequestReserveDto classRoomMeanRequestReserveDto)
        {
            // Obtiene el aula basada en la materia proporcionada en el DTO
            var classRoom = _context.ClassRooms.Where(cr => cr.Subjects.Contains(_context.Subjects.Where(s => s.NameSub == classRoomMeanRequestReserveDto.subjectName).First())).First();

            if (classRoomMeanRequestReserveDto.reserve) // Si se desea reservar el aula
            {
                if (!classRoom.IsAviable)
                    return new ClassRoommeanRequestReserveResponseDto { success = false, message = "Fail!. ClassRoom " + classRoom.IdClassR + " is not aviable anymore." };

                classRoom.IsAviable = false; // Marca el aula como no disponible
            }
            else // Si se desea liberar el aula
            {
                if (classRoom.IsAviable)
                    return new ClassRoommeanRequestReserveResponseDto { success = false, message = "Fail!. ClassRoom " + classRoom.IdClassR + " is aviable already." };

                classRoom.IsAviable = true; // Marca el aula como disponible nuevamente
            }

            // Obtiene el usuario basado en el nombre de usuario proporcionado en el DTO
            var User = _context.Users.Where(u => u.UserName == classRoomMeanRequestReserveDto.userName).FirstOrDefault();

            // Obtiene al profesor asociado al usuario
            var professor = _context.Professors.Where(p => p.UserId == User.Id).First();

            if (classRoomMeanRequestReserveDto.reserve) // Si se desea reservar un medio auxiliar
            {
                var list = _context.ProfessorClassRooms.Where(p => p.IdClassR == classRoom.IdClassR && p.IdProf == professor.IdProf).ToList();

                if (list.Count() == 0)
                {
                    // Agrega la relación entre el profesor y el aula si no existe ya
                    _context.ProfessorClassRooms.Add(new ProfessorClassRoom { IdProf = professor.IdProf, IdClassR = classRoom.IdClassR, professor = professor, classRoom = classRoom });
                }
            }

            Dictionary<List<AuxiliaryMeans>, int> AuxMean = new Dictionary<List<AuxiliaryMeans>, int>();

            int i = 0;
            foreach (var rm in classRoomMeanRequestReserveDto.reserveMeans)
            {
                if (classRoomMeanRequestReserveDto.reserve)
                {
                    AuxMean.Add(_context.AuxiliaryMeans.Where(am => am.NameMean == rm.Key && am.isAviable).ToList(), rm.Value);
                }
                else
                {
                    AuxMean.Add(_context.AuxiliaryMeans.Where(am => am.NameMean == rm.Key && !am.isAviable).ToList(), rm.Value);
                }

                foreach (var am in AuxMean)
                {
                    if (am.Key.Count() < am.Value)
                        return new ClassRoommeanRequestReserveResponseDto { success = false, message = "Fail!. Some AuxMean is not aviable anymore." };

                    for (int j = 0; j < am.Value; j++)
                    {
                        if (classRoomMeanRequestReserveDto.reserve)
                        {
                            am.Key[j].isAviable = false; // Marca los medios auxiliares como no disponibles al reservarlos
                        }
                        else
                        {
                            am.Key[j].isAviable = true; // Marca los medios auxiliares como disponibles al liberarlos
                        }
                    }
                }
            }

            if (AuxMean.Count() > 0)
            {
                professor.UseAuxMean = true; // Indica que se están utilizando medios auxiliares en esta reserva
            }

            _context.SaveChanges(); // Guarda todos los cambios realizados en la base de datos

            return new ClassRoommeanRequestReserveResponseDto { success = true, classRoom = classRoom.IdClassR, message = "Success!", reserveMeans = classRoomMeanRequestReserveDto.reserveMeans, subjectName = classRoomMeanRequestReserveDto.subjectName };
        }


    }
}
