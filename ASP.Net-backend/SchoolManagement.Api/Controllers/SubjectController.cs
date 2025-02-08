using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Subject;
using SchoolManagement.Application.ApplicationServices.Services;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("subject")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateSubject(SubjectDto subject)
        {
            var createdSubject = await _subjectService.CreateSubjectAsync(subject);
            return Ok(createdSubject);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "SuperAdmin, Secretary")]
        public async Task<ActionResult<IEnumerable<SubjectResponseDto>>> ListSubjects()
        {
            var subjects = await _subjectService.ListSubjectAsync();
            return Ok(subjects);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> UpdateSubject(SubjectResponseDto subject)
        {
            var updatedSubject = await _subjectService.UpdateSubjectAsync(subject);
            return Ok(updatedSubject);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteSubject(int subjectId)
        {
            var subject = await _subjectService.DeleteSubjectByIdAsync(subjectId);
            return Ok(subject);
        }
    }

}
