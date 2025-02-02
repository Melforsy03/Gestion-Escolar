using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class NotificationService : INotificationService
    {
        public async Task<NotificationResponse> GetNotification(string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
