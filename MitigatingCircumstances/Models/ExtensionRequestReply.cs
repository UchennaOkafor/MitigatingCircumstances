using System;

namespace MitigatingCircumstances.Models
{
    public class ExtensionRequestReply
    {
        public int Id { get; set; }

        public virtual ExtensionRequest ExtensionRequest { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();
    }
}
