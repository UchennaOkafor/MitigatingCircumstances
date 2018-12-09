using MitigatingCircumstances.Models;
using MitigatingCircumstances.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace MitigatingCircumstances.Repositories
{
    public class SupportTicketRepository : BaseRepository, ISupportTicketRepository
    {
        public SupportTicketRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public SupportTicket GetSupportTicketById(int id)
        {
            return Context.SupportTickets.Find(id);
        }

        public IEnumerable<SupportTicket> GetAllTicketsForTutor(string tutorId)
        {
            return Context.SupportTickets.Where(t => t.TutorAssignedTo.Id == tutorId);
        }

        public IEnumerable<SupportTicket> GetAllTicketsForStudent(string studentId)
        {
            return Context.SupportTickets.Where(t => t.StudentCreatedBy.Id == studentId);
        }

        public void SaveSupportTicket(SupportTicket supportTicket)
        {
            Context.SupportTickets.Add(supportTicket);
            SaveChanges();
        }
    }
}
