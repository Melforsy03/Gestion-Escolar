using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Services;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("classroom")]
    public class ClassRoomController : ControllerBase
    {
        private readonly IClassRoomService _classRoomService;
        public ClassRoomController(IClassRoomService classRoomService)
        {
            _classRoomService = classRoomService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateClassRoom(ClassRoomDto classRoom)
        {
            var classRoom2 = await _classRoomService.CreateClassRoomAsync(classRoom);
            return Ok(classRoom2);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "SuperAdmin, Professor")]
        public async Task<ActionResult<IEnumerable<ClassRoom>>> ListClassRoom()
        {
            var classRoom = await _classRoomService.ListClassRoomAsync();
            return Ok(classRoom);
        }

        [HttpPut]
        [Route("update")]
        [Authorize (Roles = "SuperAdmin")]
        public async Task<ActionResult> UpdateClassRoom(ClassRoomDto classRoom)
        {
            var _classRoom = await _classRoomService.UpdateClassRoomAsync(classRoom);
            return Ok(_classRoom);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteClassRoom(int classRoomId)
        {
            var classRoom = await _classRoomService.DeleteClassRoomByIdAsync(classRoomId);
            return Ok(classRoom);
        }
    }
}
