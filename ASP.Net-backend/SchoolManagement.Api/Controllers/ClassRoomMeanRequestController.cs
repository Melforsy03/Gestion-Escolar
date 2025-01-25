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

        [HttpGet]
        [Route("checkAviableClassRoomsAndMeans")]
        [Authorize(Roles = "SuperAdmin,Professor")]
        public async Task<IActionResult> CreateClassRoomMeanRequest(ClassRoomMeanRequestDto classRoomMeanRequest)
        {
            var aviableClassRoomsAndMeans = await _classRoomMeanRequestService.GetAviableClassRoomMeanAsync(classRoomMeanRequest);
            return Ok(aviableClassRoomsAndMeans);
        }

        [HttpPost]
        [Route("reserveClassRoomAndMeans")]
        [Authorize(Roles = "SuperAdmin,Professor")]
        public async Task<IActionResult> ReserveClassRoomAndMean (ClassRoomMeanRequestDto classRoomMeanRequest)
        {
            var answer = await _classRoomMeanRequestService.ReserveClassRoomAndMeanAsync(classRoomMeanRequest);
            return Ok(answer);
        }
    }

}
