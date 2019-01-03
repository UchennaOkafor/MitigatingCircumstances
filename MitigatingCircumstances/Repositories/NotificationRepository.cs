using Google.Cloud.Datastore.V1;
using MitigatingCircumstances.Models.GoogleEntity;
using MitigatingCircumstances.Repositories.Interface;
using System;
using System.Collections.Generic;

namespace MitigatingCircumstances.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DatastoreDb _db;
        private readonly string Kind;
        private readonly KeyFactory _keyFactory;

        public NotificationRepository(DatastoreDb db)
        {
            _db = db;
            Kind = "Notification";
            _keyFactory = db.CreateKeyFactory(Kind);
        }

        public void InsertNotification(Notification notification)
        {
            notification.Key = _keyFactory.CreateIncompleteKey();
            _db.Insert(notification.ToEntity());
        }

        public List<Notification> GetUnreadNotificationsForUser(string userId)
        {
            var query = new Query(Kind)
            {
                Filter = Filter.And(Filter.Equal("user_id", userId), Filter.Equal("is_read", false)),
                Order = { { "created_at", PropertyOrder.Types.Direction.Descending } }
            };

            var results = _db.RunQueryLazily(query).GetAllResults();
            var notifications = new List<Notification>();

            foreach (Entity entity in results.Entities)
            {
                notifications.Add(new Notification
                {
                    Key = entity.Key,
                    CreatedAt = (DateTime)entity["created_at"],
                    IsRead = (bool)entity["is_read"],
                    Text = (string)entity["text"],
                    UserId = (string)entity["user_id"]
                });
            }

            return notifications;
        }

        public void Delete(Entity entity)
        {
            _db.Delete(entity);
        }

        public void Delete(Key key)
        {
            _db.Delete(key);
        }
    }
}
