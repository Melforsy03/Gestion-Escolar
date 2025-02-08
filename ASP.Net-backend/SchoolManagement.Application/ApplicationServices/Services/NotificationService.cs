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
            // Inicializa listas para almacenar notificaciones de profesores y medios
            List<ProfessorNotifications> profNot = new List<ProfessorNotifications>();
            List<MeanNotifications> meanNot = new List<MeanNotifications>();

            // Crea un objeto de respuesta para las notificaciones
            NotificationResponse notResponse = new NotificationResponse();
            notResponse.messages = new List<string>(); // Inicializa la lista de mensajes de notificación

            // Obtiene el rol del usuario basado en su UserId
            string role = _context.Roles.Where(r => r.Id ==
                _context.UserRoles.Where(ur => ur.UserId ==
                    _context.Users.Where(u => u.UserName == UserId).FirstOrDefault().Id).FirstOrDefault().RoleId)
                .FirstOrDefault().Name.ToString();

            // Si el rol es Admin, busca notificaciones relacionadas con medios
            if (role == Role.Admin)
            {
                // Obtiene las notificaciones de medios que no han sido enviadas
                meanNot = _context.MeanNotifications.Where(mn => !mn.BeenSended).ToList();

                // Si hay notificaciones de medios, agrega mensajes a la respuesta
                if (meanNot.Count() > 0)
                {
                    foreach (var m in meanNot)
                    {
                        notResponse.messages.Add("El medio " + m.MeanName + " ha tenido más de 3 mantenimientos en el último año. Su id es " + m.MeanID);

                        // Marca la notificación como enviada
                        _context.MeanNotifications.Where(mn => mn.MeanNotId == m.MeanNotId).FirstOrDefault().BeenSended = true;
                    }
                }
            }

            // Si el rol es SuperAdmin, busca notificaciones relacionadas con profesores
            if (role == Role.SuperAdmin)
            {
                // Obtiene las notificaciones de profesores que no han sido enviadas
                profNot = _context.ProfessorNotifications.Where(pn => !pn.BeenSended).ToList();

                // Si hay notificaciones de profesores, agrega mensajes a la respuesta
                if (profNot.Count() > 0)
                {
                    foreach (var prof in profNot)
                    {
                        notResponse.messages.Add("El profesor " + prof.ProfName + " lleva 5 años recibiendo valoraciones inferiores a 3. Su Id=" + prof.IdProf);

                        // Marca la notificación como enviada
                        _context.ProfessorNotifications.Where(pn => pn.ProfNotId == prof.ProfNotId).FirstOrDefault().BeenSended = true;
                    }
                }
            }

            return notResponse; // Retorna el objeto de respuesta con los mensajes de notificación
        }

    }
}
