using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomMeanRequest;

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
        public async Task<IActionResult> CreateClassRoomMeanRequest(ClassRoomMeanRequestGetAviableDto classRoomMeanRequest)
        {
            var aviableClassRoomsAndMeans = await _classRoomMeanRequestService.GetAviableClassRoomMeanAsync(classRoomMeanRequest);
            return Ok(aviableClassRoomsAndMeans);
        }

        [HttpPost]
        [Route("reserveClassRoomAndMeans")]
        [Authorize(Roles = "SuperAdmin, Professor")]
        public async Task<IActionResult> ReserveClassRoomAndMean (ClassRoomMeanRequestReserveDto classRoomMeanRequest)
        {
            var answer = await _classRoomMeanRequestService.ReserveClassRoomAndMeanAsync(classRoomMeanRequest);
            return Ok(answer);
        }

        [HttpGet]
        [Route("checkNotAviableClassRoomsAndMeans")]
        [Authorize(Roles = "SuperAdmin, Professor")]
        public async Task<IActionResult> CheckNotAviableClassRoomMean(ClassRoomMeanRequestGetAviableDto classRoomMeanRequest)
        {
            var answer = await _classRoomMeanRequestService.GetNotAviableClassRoomMeanAsync(classRoomMeanRequest);
            return Ok(answer);
        }
        
    }

}
