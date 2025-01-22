using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Domain.Relations;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("studentSubject")]
    public class StudentSubjectController : ControllerBase
    {
        private readonly IStudentSubjectService _studentSubjectService;

        public StudentSubjectController(IStudentSubjectService studentSubjectService)
        {
            _studentSubjectService = studentSubjectService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateStudentSubject(StudentSubjectDto studentSubject)
        {
            var createdStudentSubject = await _studentSubjectService.CreateStudentSubjectAsync(studentSubject);
            return Ok(createdStudentSubject);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<StudentSubject>>> ListStudentSubjects()
        {
            var studentSubjects = await _studentSubjectService.ListStudentSubjectAsync();
            return Ok(studentSubjects);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateStudentSubject(StudentSubjectDto studentSubject)
        {
            var updatedStudentSubject = await _studentSubjectService.UpdateStudentSubjectAsync(studentSubject);
            return Ok(updatedStudentSubject);
        }

    }
}
