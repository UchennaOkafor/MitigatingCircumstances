using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MitigatingCircumstances.Models
{
    public class SupportTicketReply
    {
        public int Id { get; set; }

        public virtual SupportTicket SupportTicket { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();
    }
}
