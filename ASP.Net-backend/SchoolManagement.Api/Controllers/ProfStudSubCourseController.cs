using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfStudSubCourse;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("profstudsubcourse")]
    public class ProfStudSubCourseController : ControllerBase
    {
        private readonly IProfStudSubCourseService _profStudSubCourseService;

        public ProfStudSubCourseController(IProfStudSubCourseService profStudSubCourseService)
        {
            _profStudSubCourseService = profStudSubCourseService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateProfStudSubCourse(ProfStudSubCourseDto profStudSubCourseDto)
        {
            var createdProfStudSubCourse = await _profStudSubCourseService.CreateProfStudSubCourseAsync(profStudSubCourseDto);
            return Ok(createdProfStudSubCourse);
        }
        [HttpGet]
        [Route("listProfessorsByStudent")]
        public async Task<ActionResult<IEnumerable<ProfStudSubCourseConsultResponseDto>>> GetProfessorsByStudent (string userName){

            var professorsInfo = await _profStudSubCourseService.GetProfessors(userName);
            return Ok(professorsInfo);
        }
        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<ProfStudSubCourseResponseDto>>> ListProfStudSubCourses()
        {
            var profStudSubCourses = await _profStudSubCourseService.ListProfStudSubCoursesAsync();
            return Ok(profStudSubCourses);
        }
        [HttpPost]
        [Route("listByProfessor")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<ProfStudSubCourseResponseDto>>> ListProfStudSubCoursesByProf(ProfStudSubCourseConsultDto profStudSubCourseConsultDto)
        {
            var profStudSubCourses = await _profStudSubCourseService.ListProfStudSubCoursesByProfAsync(profStudSubCourseConsultDto);
            return Ok(profStudSubCourses);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateProfStudSubCourse(ProfStudSubCourseResponseDto profStudSubCourseDto)
        {
            var updatedProfStudSubCourse = await _profStudSubCourseService.UpdateProfStudSubCourseAsync(profStudSubCourseDto);
            return Ok(updatedProfStudSubCourse);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteProfStudSubCourse(int id)
        {
            await _profStudSubCourseService.DeleteProfStudSubCourseByIdAsync(id);
            return Ok();
        }
    }

}
