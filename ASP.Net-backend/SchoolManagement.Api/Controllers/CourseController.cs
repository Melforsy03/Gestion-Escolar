using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Course;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("course")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "SuperAdmin, Secretary")]
        public async Task<IActionResult> CreateCourse(CourseDto course)
        {
            var createdCourse = await _courseService.CreateCourseAsync(course);
            return Ok(createdCourse);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "SuperAdmin, Secretary")]
        public async Task<ActionResult<IEnumerable<CourseResponseDto>>> ListCourses()
        {
            var courses = await _courseService.ListCoursesAsync();
            return Ok(courses);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin, Secretary")]
        public async Task<ActionResult> UpdateCourse(CourseResponseDto course)
        {
            var updatedCourse = await _courseService.UpdateCourseAsync(course);
            return Ok(updatedCourse);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            var course = await _courseService.DeleteCourseByIdAsync(courseId);
            return Ok(course);
        }
    }
}
