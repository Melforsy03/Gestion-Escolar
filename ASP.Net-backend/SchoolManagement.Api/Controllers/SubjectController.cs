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
        public SubjectController(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateSubject(SubjectDto subject)
        {
            var subject2 = await _subjectService.CreateSubjectAsync(subject);
            return Ok(subject2);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize (Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<Subject>>> ListSubject()
        {
            var subject = await _subjectService.ListSubjectAsync();
            return Ok(subject);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateSubject(SubjectDto subject)
        {
            var _subject = await _subjectService.UpdateSubjectAsync(subject);
            return Ok(_subject);
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
