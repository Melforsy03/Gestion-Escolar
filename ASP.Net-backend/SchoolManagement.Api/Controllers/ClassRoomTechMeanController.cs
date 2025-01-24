using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("classRoomTechMean")]
    public class ClassRoomTechMeanController : ControllerBase
    {
        private readonly IClassRoomTechMeanService _classRoomTechMeanService;

        public ClassRoomTechMeanController(IClassRoomTechMeanService classRoomTechMeanService)
        {
            _classRoomTechMeanService = classRoomTechMeanService;
        }

        [HttpPost]
        [Route("create")]
       // [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateClassRoomTechMean(ClassRoomTechMeanDto classRoomTechMean)
        {
            var createdClassRoomTechMean = await _classRoomTechMeanService.CreateClassRoomTechMeanAsync(classRoomTechMean);
            return Ok(createdClassRoomTechMean);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<ClassRoomTechMeanDto>>> ListClassRoomTechMeans()
        {
            var classRoomTechMeans = await _classRoomTechMeanService.ListClassRoomTechMeansAsync();
            return Ok(classRoomTechMeans);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateClassRoomTechMean(ClassRoomTechMeanDto classRoomTechMean)
        {
            var updatedClassRoomTechMean = await _classRoomTechMeanService.UpdateClassRoomTechMeanAsync(classRoomTechMean);
            return Ok(updatedClassRoomTechMean);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteClassRoomTechMean(int id)
        {
            await _classRoomTechMeanService.DeleteClassRoomTechMeanByIdAsync(id);
            return Ok();
        }
    }

}
