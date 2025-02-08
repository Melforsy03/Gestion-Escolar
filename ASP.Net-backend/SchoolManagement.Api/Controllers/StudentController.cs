using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Student;
using SchoolManagement.Application.ApplicationServices.Services;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Identity;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Secretary, SuperAdmin")]
        public async Task<IActionResult> CreateStudent(StudentDto student)
        {
            var student2 = await _studentService.CreateStudentAsync(student);
            return Ok(student2);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "Secretary, SuperAdmin, Professor")]
        public async Task<ActionResult<IEnumerable<Student>>> ListStudent()
        {
            var student = await _studentService.ListStudentAsync();
            return Ok(student);
        }
        [HttpGet]
        [Route("getBadStudents")]
        [Authorize(Roles = "Secretary, SuperAdmin, Professor")]
        public async Task<ActionResult<IEnumerable<Student>>> ListBadStudent()
        {
            var badStudents = await _studentService.GetBadStudents();
            return Ok(badStudents);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "Secretary, SuperAdmin")]
        public async Task<ActionResult> UpdateStudent(StudentResponseDto studentInfo)
        {
            var _student = await _studentService.UpdateStudentAsync(studentInfo);
            return Ok(_student);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "Secretary, SuperAdmin")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            var student = await _studentService.DeleteStudentByIdAsync(studentId);
            return Ok(student);
        }
    }
}
