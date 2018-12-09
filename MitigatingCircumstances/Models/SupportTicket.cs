using System;
using System.Collections.Generic;
using MitigatingCircumstances.Models.Enum;

namespace MitigatingCircumstances.Models
{
    public class SupportTicket
    {
        public int Id { get; set; }

        public virtual ApplicationUser StudentCreatedBy { get; set; }

        public virtual ApplicationUser TutorAssignedTo { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public virtual ICollection<UploadedDocument> UploadedDocuments { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();

        public virtual TicketStatus? Status { get; set; }
    }
}
