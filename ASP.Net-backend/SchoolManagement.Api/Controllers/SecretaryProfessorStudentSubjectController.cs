using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;

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
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateSecretaryProfessorStudentSubject(SecretaryProfessorStudentSubjectDto secretaryProfessorStudentSubject)
        {
            var createdSecretary = await _secretaryProfessorStudentSubjectService.CreateSecretaryProfessorStudentSubjectAsync(secretaryProfessorStudentSubject);
            return Ok(createdSecretary);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<SecretaryProfessorStudentSubjectDto>>> ListSecretariesProfessorStudentSubjects()
        {
            var secretaries = await _secretaryProfessorStudentSubjectService.ListSecretariesProfessorStudentSubjectsAsync();
            return Ok(secretaries);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateSecretaryProfessorStudentSubject(SecretaryProfessorStudentSubjectDto secretaryProfessorStudentSubject)
        {
            var updatedSecretary = await _secretaryProfessorStudentSubjectService.UpdateSecretaryProfessorStudentSubjectAsync(secretaryProfessorStudentSubject);
            return Ok(updatedSecretary);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteSecretaryProfessorStudentSubject(int id)
        {
            await _secretaryProfessorStudentSubjectService.DeleteSecretaryProfessorStudentSubjectByIdAsync(id);
            return Ok();
        }
    }

}
