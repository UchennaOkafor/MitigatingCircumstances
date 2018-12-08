using Google.Cloud.Datastore.V1;

namespace MitigatingCircumstances.Models.GoogleEntity
{
    public class Notification
    {
        public Key Key { get; set; }

        public string Text { get; set; }

        public Entity ToEntity()
        {
            return new Entity()
            {
                Key = Key,
                ["Text"] = Text
            };
        }
    }
}
