using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Domain.Relations;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("professorSubject")]
    public class ProfessorSubjectController : ControllerBase
    {
        private readonly IProfessorSubjectService _professorSubjectService;

        public ProfessorSubjectController(IProfessorSubjectService professorSubjectService)
        {
            _professorSubjectService = professorSubjectService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateProfessorSubject(ProfessorSubjectDto professorSubject)
        {
            var createdProfessorSubject = await _professorSubjectService.CreateProfessorSubjectAsync(professorSubject);
            return Ok(createdProfessorSubject);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<ProfessorSubject>>> ListProfessorSubjects()
        {
            var professorSubjects = await _professorSubjectService.ListProfessorSubjectAsync();
            return Ok(professorSubjects);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteProfessorSubject(int professorSubjectId)
        {
            await _professorSubjectService.DeleteProfessorSubjectByIdAsync(professorSubjectId);
            return Ok();
        }
    }


}
