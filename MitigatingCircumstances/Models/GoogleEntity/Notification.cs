using Google.Cloud.Datastore.V1;
using System;

namespace MitigatingCircumstances.Models.GoogleEntity
{
    public class Notification
    {
        public Key Key { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Entity ToEntity()
        {
            return new Entity()
            {
                Key = Key,
                ["text"] = Text,
                ["user_id"] = UserId,
                ["is_read"] = IsRead,
                ["created_at"] = CreatedAt
            };
        }
    }
}
