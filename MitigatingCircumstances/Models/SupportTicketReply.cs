using System;

namespace MitigatingCircumstances.Models
{
    public class SupportTicketReply
    {
        public int Id { get; set; }

        public virtual SupportTicket Ticket { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();
    }
}
