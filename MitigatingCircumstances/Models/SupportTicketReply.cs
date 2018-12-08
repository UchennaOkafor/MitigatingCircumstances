using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MitigatingCircumstances.Models
{
    public class SupportTicketReply
    {
        public int Id { get; set; }

        public SupportTicket SupportTicket { get; set; }

        public ApplicationUser User { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }
    }
}
