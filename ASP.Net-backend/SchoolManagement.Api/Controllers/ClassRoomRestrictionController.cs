using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("classRoomRestriction")]
    public class ClassRoomRestrictionController : ControllerBase
    {
        private readonly IClassRoomRestrictionService _classRoomRestrictionService;

        public ClassRoomRestrictionController(IClassRoomRestrictionService classRoomRestrictionService)
        {
            _classRoomRestrictionService = classRoomRestrictionService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateClassRoomRestriction(ClassRoomRestrictionDto classRoomRestriction)
        {
            var createdClassRoomRestriction = await _classRoomRestrictionService.CreateClassRoomRestrictionAsync(classRoomRestriction);
            return Ok(createdClassRoomRestriction);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<ClassRoomRestrictionDto>>> ListClassRoomRestrictions()
        {
            var classRoomRestrictions = await _classRoomRestrictionService.ListClassRoomRestrictionsAsync();
            return Ok(classRoomRestrictions);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateClassRoomRestriction(ClassRoomRestrictionDto classRoomRestriction)
        {
            var updatedClassRoomRestriction = await _classRoomRestrictionService.UpdateClassRoomRestrictionAsync(classRoomRestriction);
            return Ok(updatedClassRoomRestriction);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteClassRoomRestriction(int id)
        {
            await _classRoomRestrictionService.DeleteClassRoomRestrictionByIdAsync(id);
            return Ok();
        }
    }

}
