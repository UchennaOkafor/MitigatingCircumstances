using Google.Cloud.Datastore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MitigatingCircumstances.Models
{
    public class Ticket
    {
        public Key Key { get; set; }

        public Student Student { get; set; }

        public string Text { get; set; }

        public Entity ToEntity()
        {
            return new Entity()
            {
                Key = Key,
                ["Student"] = Student.Key,
                ["Text"] = Text
            };
        }
    }
}
