using MitigatingCircumstances.Models.Enum;
using System;

namespace MitigatingCircumstances.Models
{
    public class SupportTicket
    {
        public int Id { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        public ApplicationUser AssignedTo { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime Created { get; set; }

        public TicketStatus Status { get; set; }
    }
}
