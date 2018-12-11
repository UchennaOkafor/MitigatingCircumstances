using Microsoft.EntityFrameworkCore;
using MitigatingCircumstances.Models;
using MitigatingCircumstances.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace MitigatingCircumstances.Repositories
{
    public class ExtensionRequestRepository : BaseRepository, IExtensionRequestRepository
    {
        public ExtensionRequestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public ExtensionRequest GetExtensionRequestById(int id)
        {
            return Context.ExtensionRequests.Find(id);
        }

        public IEnumerable<ExtensionRequest> GetExtensionRequestsAssignedTo(string tutorId)
        {
            return Context.ExtensionRequests.Where(t => t.TutorAssignedTo.Id == tutorId);
        }

        public IEnumerable<ExtensionRequest> GetExtensionRequestsCreatedBy(string studentId)
        {
            return Context.ExtensionRequests.Where(t => t.StudentCreatedBy.Id == studentId);
        }

        public void SaveExtensionRequest(ExtensionRequest extensionRequest)
        {
            Context.ExtensionRequests.Add(extensionRequest);
            SaveChanges();
        }
    }
}
