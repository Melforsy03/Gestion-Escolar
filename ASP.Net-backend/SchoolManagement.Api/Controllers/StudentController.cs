using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Services;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateStudent(StudentDto student)
        {
            var student2 = await _studentService.CreateStudentAsync(student);
            return Ok(student2);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize (Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<Student>>> ListStudent()
        {
            var student = await _studentService.ListStudentAsync();
            return Ok(student);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateStudent(StudentDto student)
        {
            var _student = await _studentService.UpdateStudentAsync(student);
            return Ok(_student);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            await _studentService.DeleteStudentByIdAsync(studentId);
            return Ok();
        }
    }
}
