using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorStudentSubject;

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
        [Route("givenote")]
        
        [Authorize(Roles = "SuperAdmin, Professor")]
        public async Task<IActionResult> CreateProfessorStudentSubject(ProfessorStudentSubjectDto professorStudentSubject)
        {
            var createdProfessorStudentSubject = await _professorStudentSubjectService.CreateProfessorStudentSubjectAsync(professorStudentSubject);
            return Ok(createdProfessorStudentSubject);
        }

        [HttpGet]
        [Route("list")]
        
        [Authorize(Roles = "SuperAdmin, Professor, Secretary")]
        public async Task<ActionResult<IEnumerable<ProfessorStudentSubjectResponseDto>>> ListProfessorStudentSubjects()
        {
            var professorStudentSubject = await _professorStudentSubjectService.ListProfessorStudentSubjectAsync();
            return Ok(professorStudentSubject);
        }

        [HttpGet]
        [Route("getsubjects")]

        [Authorize(Roles = "SuperAdmin, Professor, Secretary")]
        public async Task<ActionResult> GetSubjects(string UserName)
        {
            var professorStudentSubject = await _professorStudentSubjectService.GetSubjectsOfProfessorAsync(UserName);
            return Ok(professorStudentSubject);
        }

        [HttpGet]
        [Route("getstudents")]

        [Authorize(Roles = "SuperAdmin, Professor, Secretary")]
        public async Task<ActionResult> GetSubjects(int IdSub)
        {
            var professorStudentSubject = await _professorStudentSubjectService.GetStudentsForSubjectAsync(IdSub);
            return Ok(professorStudentSubject);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin, Professor, Secretary")]
        public async Task<ActionResult> UpdateProfessorStudentSubject(ProfessorStudentSubjectResponseDto professorStudentSubject)
        {
            var updatedProfessorStudentSubject = await _professorStudentSubjectService.UpdateProfessorStudentSubjectAsync(professorStudentSubject);
            return Ok(updatedProfessorStudentSubject);
        }
    }
}
