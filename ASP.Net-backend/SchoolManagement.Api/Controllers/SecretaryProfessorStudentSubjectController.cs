using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.SecretaryProfessorStudentSubject;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("secretaryProfessorStudentSubject")]
    public class SecretaryProfessorStudentSubjectController : ControllerBase
    {
        private readonly ISecretaryProfessorStudentSubjectService _secretaryProfessorStudentSubjectService;

        public SecretaryProfessorStudentSubjectController(ISecretaryProfessorStudentSubjectService secretaryProfessorStudentSubjectService)
        {
            _secretaryProfessorStudentSubjectService = secretaryProfessorStudentSubjectService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateSecretaryProfessorStudentSubject(SecretaryProfessorStudentSubjectDto secretaryProfessorStudentSubject)
        {
            var createdSecretary = await _secretaryProfessorStudentSubjectService.CreateSecretaryProfessorStudentSubjectAsync(secretaryProfessorStudentSubject);
            return Ok(createdSecretary);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<SecretaryProfessorStudentSubjectResponseDto>>> ListSecretariesProfessorStudentSubjects()
        {
            var secretaries = await _secretaryProfessorStudentSubjectService.ListSecretariesProfessorStudentSubjectsAsync();
            return Ok(secretaries);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateSecretaryProfessorStudentSubject(SecretaryProfessorStudentSubjectResponseDto secretaryProfessorStudentSubject)
        {
            var updatedSecretary = await _secretaryProfessorStudentSubjectService.UpdateSecretaryProfessorStudentSubjectAsync(secretaryProfessorStudentSubject);
            return Ok(updatedSecretary);
        }

    }

}
