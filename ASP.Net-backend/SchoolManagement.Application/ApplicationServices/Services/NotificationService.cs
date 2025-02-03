using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Notification;
using SchoolManagement.Domain.Notifications;
using SchoolManagement.Domain.Role;
using SchoolManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class NotificationService : INotificationService
    {
        private readonly Context _context;
        public NotificationService(Context context) {
            _context = context;
        }
        public async Task<NotificationResponse> GetNotification(string UserId)
        {
            List<ProfessorNotifications> profNot = new List<ProfessorNotifications>();
            List<MeanNotifications> meanNot = new List<MeanNotifications>();
            NotificationResponse notResponse = new NotificationResponse();
            notResponse.messages = new List<string>();
            string role = _context.Roles.Where(r => r.Id == _context.UserRoles.Where(ur => ur.UserId == _context.Users.Where(u => u.UserName == UserId).FirstOrDefault().Id).FirstOrDefault().RoleId).FirstOrDefault().Name.ToString();
         
            if(role == Role.Admin)
            {
                meanNot = _context.MeanNotifications.Where(mn => !mn.BeenSended).ToList();

                if(meanNot.Count() > 0)
                {
                    foreach(var m in meanNot)
                    {
                        notResponse.messages.Add("El medio " + m.MeanName + " ha tenido mas de 3 mantenimientos en el ultimo year. Su id es " + m.MeanID);
                        _context.MeanNotifications.Where(mn => mn.MeanNotId == m.MeanNotId).FirstOrDefault().BeenSended = true;
                    }
                }
            }



            if(role == Role.SuperAdmin)
            {
                profNot = _context.ProfessorNotifications.Where(pn => !pn.BeenSended).ToList();


                if (profNot.Count() > 0)
                {
                    foreach (var prof in profNot)
                    {
                        notResponse.messages.Add("El professor " + prof.ProfName + " lleva 5y recibiendo valoraciones inferiores a 3!!. Su Id=" + prof.IdProf);
                        _context.ProfessorNotifications.Where(pn => pn.ProfNotId == prof.ProfNotId).FirstOrDefault().BeenSended = true;
                    }
                }
            }
            

            return notResponse;
        }
    }
}
