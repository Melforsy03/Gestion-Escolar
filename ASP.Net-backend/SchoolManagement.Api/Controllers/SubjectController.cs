using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
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
        //[Authorize(Roles = "Professor")]
        public async Task<IActionResult> CreateSubject(SubjectDto subject)
        {
            var createdSubject = await _subjectService.CreateSubjectAsync(subject);
            return Ok(createdSubject);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "Professor")]
        public async Task<ActionResult<IEnumerable<Subject>>> ListSubjects()
        {
            var subjects = await _subjectService.ListSubjectAsync();
            return Ok(subjects);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateSubject(SubjectDto subject)
        {
            var updatedSubject = await _subjectService.UpdateSubjectAsync(subject);
            return Ok(updatedSubject);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteSubject(int subjectId)
        {
            await _subjectService.DeleteSubjectByIdAsync(subjectId);
            return Ok();
        }
    }

}
