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
            List<ProfessorClassRoomRequest> professorClassRoomRequests = new List<ProfessorClassRoomRequest>();
            var professors = _context.Professors.Where(p => p.Spec == spec).ToList();
            List<ProfessorClassRoom> professorClassRoom = new List<ProfessorClassRoom>();

            foreach(var prof in professors)
            {
                professorClassRoom.AddRange(_context.ProfessorClassRooms.Where(pcr => pcr.IdProf == prof.IdProf).ToList());
            }
      

            foreach(var pc in professorClassRoom)
            {
                ProfessorClassRoomRequest temp = new ProfessorClassRoomRequest();
                temp.NameProf = _context.Professors.Where(p => p.IdProf == pc.IdProf).FirstOrDefault().NameProf;
                temp.Spec = _context.Professors.Where(p => p.IdProf == pc.IdProf).FirstOrDefault().Spec;
                Dictionary<string, string> list = new Dictionary<string, string>();
                var techMeansClassRoom = _context.ClassRoomTechMeans.Where(crt => crt.IdClassRoom == pc.IdClassR).ToList();
               
                foreach (var tech in techMeansClassRoom)
                {
                    var mean = _context.TechnologicalMeans.Where(p => p.IdMean == tech.IdTechMean).FirstOrDefault();
                    list.Add(mean.NameMean, mean.State);
                }

                temp.ClassRoomsAndMeans = new Dictionary<int, Dictionary<string, string>>();
                temp.ClassRoomsAndMeans.Add(pc.IdClassR, list);

                professorClassRoomRequests.Add(temp);
            }

            return professorClassRoomRequests;
        }

        public async Task<ProfessorClassRoomGetSpecRequest> GetSpecs()
        {   
            ProfessorClassRoomGetSpecRequest specs = new ProfessorClassRoomGetSpecRequest();

            specs.Spec = _context.Professors.GroupBy(p => p.Spec).Select(g=> g.Key).ToList();
            
            return specs;
        }

        
    }
}
