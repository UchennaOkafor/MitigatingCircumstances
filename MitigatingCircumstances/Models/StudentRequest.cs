using Google.Cloud.Datastore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MitigatingCircumstances.Models
{
    public class StudentRequest
    {
        public Key Id { get; set; }

        public Student Student { get; set; }

        public string Text { get; set; }
    }
}
