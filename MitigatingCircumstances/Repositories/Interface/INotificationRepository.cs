using System.Collections.Generic;
using MitigatingCircumstances.Models.GoogleEntity;

namespace MitigatingCircumstances.Repositories.Interface
{
    public interface INotificationRepository
    {
        void InsertNotification(Notification notification);

        List<Notification> GetUnreadNotificationsForUser(string userId);
    }
}