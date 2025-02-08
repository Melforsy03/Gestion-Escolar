using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorClassRoom;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("professorClassRoom")]
    public class ProfessorClassRoomController: ControllerBase
    {
        private readonly IProfessorClassRoomService _professorClassRoomService;
        public ProfessorClassRoomController(IProfessorClassRoomService professorClassRoom) 
        {
            _professorClassRoomService = professorClassRoom;
        }

        [HttpGet]
        [Route("getSpecs")]
        [Authorize(Roles ="SuperAdmin")]
        public async Task<ActionResult<ProfessorClassRoomGetSpecRequest>> GetSpecs()
        {
            var specs = await _professorClassRoomService.GetSpecs();
            return Ok(specs);
        }

        [HttpGet]
        [Route("getProfessorsBySpec")]
        [Authorize(Roles ="SuperAdmin")]
        public async Task<ActionResult<IEnumerable<ProfessorClassRoomRequest>>> GetProfessorsBySpec(string spec)
        {
            var professorsBySpec = await _professorClassRoomService.GetAllProfessorsBySpec(spec);
            return Ok(professorsBySpec);
        }

    }
}
