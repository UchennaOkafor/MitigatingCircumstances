using MitigatingCircumstances.Models;
using System.Collections.Generic;

namespace MitigatingCircumstances.Repositories.Base
{
    public interface ISupportTicketRepository
    {
        SupportTicket GetSupportTicketById(int id);

        IEnumerable<SupportTicket> GetAllTicketsForTutor(string tutorId);

        IEnumerable<SupportTicket> GetAllTicketsForStudent(string studentId);

        void SaveSupportTicket(SupportTicket supportTicket);
    }
}