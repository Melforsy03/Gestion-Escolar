using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorClassRoom;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ProfessorClassRoomService : IProfessorClassRoomService
    {
        private readonly Context _context;
        
        public ProfessorClassRoomService(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProfessorClassRoomRequest>> GetAllProfessorsBySpec(string spec)
        {
            // Inicializa una lista para almacenar las solicitudes de aula de profesores
            List<ProfessorClassRoomRequest> professorClassRoomRequests = new List<ProfessorClassRoomRequest>();

            // Obtiene todos los profesores que tienen la especialidad especificada
            var professors = _context.Professors.Where(p => p.Spec == spec).ToList();

            // Inicializa una lista para almacenar las relaciones entre profesores y aulas
            List<ProfessorClassRoom> professorClassRoom = new List<ProfessorClassRoom>();

            // Itera sobre cada profesor y obtiene sus relaciones con las aulas
            foreach (var prof in professors)
            {
                professorClassRoom.AddRange(_context.ProfessorClassRooms.Where(pcr => pcr.IdProf == prof.IdProf).ToList());
            }

            // Itera sobre cada relación profesor-aula
            foreach (var pc in professorClassRoom)
            {
                ProfessorClassRoomRequest temp = new ProfessorClassRoomRequest();

                // Obtiene el nombre y la especialidad del profesor asociado a la relación
                temp.NameProf = _context.Professors.Where(p => p.IdProf == pc.IdProf).FirstOrDefault().NameProf;
                temp.Spec = _context.Professors.Where(p => p.IdProf == pc.IdProf).FirstOrDefault().Spec;

                // Inicializa un diccionario para almacenar los medios tecnológicos asociados a las aulas
                Dictionary<string, string> list = new Dictionary<string, string>();

                // Obtiene los medios tecnológicos asociados a la clase del aula
                var techMeansClassRoom = _context.ClassRoomTechMeans.Where(crt => crt.IdClassRoom == pc.IdClassR).ToList();

                // Itera sobre cada medio tecnológico asociado al aula
                foreach (var tech in techMeansClassRoom)
                {
                    // Obtiene el medio tecnológico correspondiente
                    var mean = _context.TechnologicalMeans.Where(p => p.IdMean == tech.IdTechMean).FirstOrDefault();
                    list.Add(mean.NameMean, mean.State); // Agrega el nombre y estado del medio al diccionario
                }

                // Asigna el diccionario de medios tecnológicos al objeto de solicitud del profesor
                temp.ClassRoomsAndMeans = new Dictionary<int, Dictionary<string, string>>();
                temp.ClassRoomsAndMeans.Add(pc.IdClassR, list); // Agrega la relación aula-medios al objeto

                // Agrega la solicitud del profesor a la lista de resultados
                professorClassRoomRequests.Add(temp);
            }

            // Retorna la lista de solicitudes de aula de profesores filtrados por especialidad
            return professorClassRoomRequests;
        }

        public async Task<ProfessorClassRoomGetSpecRequest> GetSpecs()
        {
            // Inicializa un objeto para almacenar las especialidades de los profesores
            ProfessorClassRoomGetSpecRequest specs = new ProfessorClassRoomGetSpecRequest();

            // Agrupa los profesores por especialidad y obtiene una lista de especialidades únicas
            specs.Spec = _context.Professors.GroupBy(p => p.Spec).Select(g => g.Key).ToList();

            return specs; // Retorna el objeto con las especialidades encontradas
        }



    }
}
