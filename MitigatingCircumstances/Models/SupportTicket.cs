using MitigatingCircumstances.Models.Enum;
using System;

namespace MitigatingCircumstances.Models
{
    public class SupportTicket
    {
        public int Id { get; set; }

        public virtual ApplicationUser StudentCreatedBy { get; set; }

        public virtual ApplicationUser TeacherAssignedTo { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();

        public virtual TicketStatus? Status { get; set; }
    }
}
