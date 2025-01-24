using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("classroommeans")]
    public class ClassRoomMeanRequestController : ControllerBase
    {
        private readonly IClassRoomMeanRequestService _classRoomMeanRequestService;

        public ClassRoomMeanRequestController(IClassRoomMeanRequestService classRoomMeanRequestService)
        {
            _classRoomMeanRequestService = classRoomMeanRequestService;
        }

        [HttpPost]
        [Route("classroommeanrequest")]
       // [Authorize(Roles = "SuperAdmin,Professor")]
        public async Task<IActionResult> CreateClassRoomMeanRequest(ClassRoomMeanRequestDto classRoomMeanRequest)
        {
            var createdClassRoomMeanRequest = await _classRoomMeanRequestService.GetAviableClassRoomMean(classRoomMeanRequest);
            return Ok(createdClassRoomMeanRequest);
        }
    }

}
