using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("notifiction")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get(string UserId)
        {
            var message = await _notificationService.GetNotification(UserId);
            return Ok(message);
        }
    }
}
