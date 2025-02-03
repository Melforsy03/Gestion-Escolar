using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Professor;
using SchoolManagement.Application.Common;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly Context _context;
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public ProfessorService(IProfessorRepository professorRepository, IMapper mapper, Triggers trigger, Context context)
        {
            _professorRepository = professorRepository;
            _context = context;
            _mapper = mapper;
            _trigger = trigger;
        }

        public async Task<ProfessorsEvaluations> GetGoodProfessors()
        {
            // Inicializa una lista para almacenar las evaluaciones de profesores
            var professors = _context.Professors.ToList(); // Obtiene todos los profesores
            ProfessorsEvaluations profEval = new ProfessorsEvaluations();
            profEval.professorsAndSubjects = new Dictionary<string, List<string>>(); // Inicializa el diccionario para almacenar profesores y sus materias

            // Itera sobre cada profesor para calcular su evaluación
            foreach (var prof in professors)
            {
                // Obtiene las evaluaciones de los estudiantes para el profesor actual
                var evaluation = _context.ProfStudSubCourses.Where(pssc => pssc.IdProf == prof.IdProf).Select(p => p.Evaluation).ToList();

                // Verifica si la evaluación promedio es mayor a 8
                if (_trigger.GetAverage(evaluation) > 8)
                {
                    // Obtiene las materias que imparte el profesor
                    var profSubjects = _context.ProfessorSubjects.Where(ps => ps.IdProf == prof.IdProf);
                    List<string> subjects = new List<string>();

                    // Agrega los nombres de las materias al listado
                    foreach (var ps in profSubjects)
                    {
                        subjects.Add(_context.Subjects.Where(s => s.IdSub == ps.IdSub).First().NameSub);
                    }

                    // Agrega el profesor y sus materias al diccionario de resultados
                    profEval.professorsAndSubjects.Add(prof.NameProf, subjects);
                }
            }

            return profEval; // Retorna las evaluaciones de los buenos profesores
        }

        public async Task<ProfessorCreateResponseDto> CreateProfessorAsync(ProfessorDto professorDto)
        {
            // Mapea el DTO del profesor a la entidad de dominio Professor
            var professor = _mapper.Map<Professor>(professorDto);

            // Registra al usuario asociado al profesor y obtiene su información
            (User, string) User = await _trigger.RegisterUser(professorDto.NameProf, "Professor");

            professor.UserId = User.Item1.Id; // Asigna el ID del usuario al profesor

            // Crea el nuevo profesor en la base de datos
            var savedProfessor = await _professorRepository.CreateAsync(professor);

            // Mapea el profesor guardado de vuelta a un DTO
            professorDto = _mapper.Map<ProfessorDto>(savedProfessor);

            // Crea la respuesta para el nuevo profesor creado
            ProfessorCreateResponseDto answer = new ProfessorCreateResponseDto();
            answer.Id = professor.IdProf; // Asigna el ID del profesor a la respuesta
            answer.professor = professorDto; // Asigna el DTO del profesor a la respuesta
            answer.UserName = User.Item1.UserName; // Asigna el nombre de usuario a la respuesta
            answer.Password = User.Item2; // Asigna la contraseña a la respuesta

            return answer; // Retorna la respuesta con los detalles del nuevo profesor creado
        }

        public async Task<ProfessorResponseDto> DeleteProfessorByIdAsync(int professorId)
        {
            // Obtiene el profesor por su ID
            var professor = _professorRepository.GetById(professorId);

            // Verifica si el profesor ya está marcado como eliminado
            if (professor.IsDeleted)
            {
                return null; // Retorna null si ya está eliminado
            }

            professor.IsDeleted = true; // Marca el profesor como eliminado

            // Actualiza el estado en la base de datos
            await _professorRepository.UpdateAsync(professor);

            // Crea un DTO de respuesta para el profesor eliminado
            ProfessorResponseDto answer = new ProfessorResponseDto();
            answer.Id = professor.IdProf; // Asigna el ID del profesor a la respuesta
            answer.professor = _mapper.Map<ProfessorDto>(professor); // Mapea y asigna el DTO del profesor

            return answer; // Retorna la respuesta con los detalles del profesor eliminado
        }

        public async Task<IEnumerable<ProfessorResponseDto>> ListProfessorAsync()
        {
            // Obtiene todos los profesores desde el repositorio
            var professors = await _professorRepository.ListAsync();

            var list = professors.ToList();
            List<ProfessorResponseDto> Professors_List = new List<ProfessorResponseDto>(); // Inicializa una lista para almacenar los DTOs

            // Itera sobre cada profesor y agrega solo los no eliminados a la lista de resultados
            for (int i = 0; i < professors.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    ProfessorResponseDto professorResponse = new ProfessorResponseDto();
                    professorResponse.Id = list[i].IdProf; // Asigna el ID del profesor a la respuesta

                    professorResponse.professor = _mapper.Map<ProfessorDto>(list[i]); // Mapea y asigna el DTO del profesor

                    Professors_List.Add(professorResponse); // Agrega a la lista de resultados
                }

            }

            return Professors_List; // Retorna la lista de DTOs de profesores no eliminados
        }

        public async Task<IEnumerable<ProfessorsBadResponse>> GetBadProfessors()
        {
            // Obtiene los IDs de los profesores que tienen castigos registrados agrupados por ID de profesor
            List<int> badProfessorsId = _context.ProfessorsPunishments.GroupBy(p => p.IdProf).Select(g => g.Key).ToList();

            List<ProfessorsBadResponse> badProfessors = new List<ProfessorsBadResponse>();

            foreach (var bp in badProfessorsId)
            {
                var professor = _context.Professors.Where(p => p.IdProf == bp).FirstOrDefault(); // Obtiene el profesor correspondiente

                ProfessorsBadResponse temp = new ProfessorsBadResponse();
                temp.UseAuxMean = professor.UseAuxMean; // Almacena si utiliza medios auxiliares
                temp.NameProf = professor.NameProf; // Almacena el nombre del profesor

                // Obtiene la fecha del primer castigo registrado para este profesor
                temp.PunishmentDate = _context.ProfessorsPunishments.Where(pp => pp.IdProf == bp).OrderBy(s => s.PunishmentDate).FirstOrDefault().PunishmentDate;

                var evals = _context.ProfStudSubCourses.Where(pssc => pssc.IdProf == bp).OrderBy(e => e.Evaluation).Select(g => g.Evaluation).ToList();

                if (evals.Count() >= 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        temp.evals[i] = evals[i];  // Almacena las tres primeras evaluaciones si hay suficientes registros
                    }
                }
                else
                {
                    for (int i = 0; i < evals.Count(); i++)
                    {
                        temp.evals[i] = evals[i];  // Almacena todas las evaluaciones si son menos de tres
                    }
                }

                badProfessors.Add(temp);  // Agrega al listado de profesores malos

            }

            return badProfessors;  // Retorna la lista de profesores con castigos registrados
        }

        public async Task<ProfessorResponseDto> UpdateProfessorAsync(ProfessorResponseDto professorInfo)
        {
            // Obtiene el profesor por su ID desde el DTO
            var professor = _professorRepository.GetById(professorInfo.Id);

            if (professor.IsDeleted)
            {
                return null;  // Retorna null si el profesor está eliminado
            }

            if (professor.Salary > professorInfo.professor.Salary)
            {
                _context.ProfessorsPunishments.Add(new ProfessorPunishment { IdProf = professor.IdProf, Professor = professor, PunishmentDate = DateTime.Now });
                // Agrega un castigo si se reduce el salario del profesor.
            }

            _mapper.Map(professorInfo.professor, professor);  // Mapea los cambios desde el DTO a la entidad existente

            await _professorRepository.UpdateAsync(professor);  // Actualiza la entidad en la base de datos

            ProfessorResponseDto answer = new ProfessorResponseDto();
            answer.Id = professor.IdProf;  // Asigna el ID del profesor a la respuesta

            answer.professor = _mapper.Map<ProfessorDto>(professor);  // Mapea y asigna el DTO del profesor

            _context.SaveChanges();  // Guarda todos los cambios realizados en el contexto

            return answer;  // Retorna la respuesta con los detalles actualizados del profesor.
        }

    }
}