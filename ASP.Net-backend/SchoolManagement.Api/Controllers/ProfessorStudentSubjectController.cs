using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("professorStudentSubject")]
    public class ProfessorStudentSubjectController : ControllerBase
    {
        private readonly IProfessorStudentSubjectService _professorStudentSubjectService;

        public ProfessorStudentSubjectController(IProfessorStudentSubjectService professorStudentSubjectService)
        {
            _professorStudentSubjectService = professorStudentSubjectService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateProfessorStudentSubject(ProfessorStudentSubjectDto professorStudentSubject)
        {
            var createdProfessorStudentSubject = await _professorStudentSubjectService.CreateProfessorStudentSubjectAsync(professorStudentSubject);
            return Ok(createdProfessorStudentSubject);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<ProfessorStudentSubjectDto>>> ListProfessorStudentSubjects()
        {
            var professorStudentSubject = await _professorStudentSubjectService.ListProfessorStudentSubjectAsync();
            return Ok(professorStudentSubject);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateProfessorStudentSubject(ProfessorStudentSubjectDto professorStudentSubject)
        {
            var updatedProfessorStudentSubject = await _professorStudentSubjectService.UpdateProfessorStudentSubjectAsync(professorStudentSubject);
            return Ok(updatedProfessorStudentSubject);
        }
    }
}
