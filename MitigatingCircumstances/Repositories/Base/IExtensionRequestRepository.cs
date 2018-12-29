using Microsoft.AspNetCore.Http;
using MitigatingCircumstances.Models;
using System.Collections.Generic;

namespace MitigatingCircumstances.Repositories.Base
{
    public interface IExtensionRequestRepository
    {
        ExtensionRequest GetExtensionRequestById(int id);

        IEnumerable<ExtensionRequest> GetExtensionRequestsAssignedTo(string tutorId);

        IEnumerable<ExtensionRequest> GetExtensionRequestsCreatedBy(string studentId);

        List<UploadedDocument> UploadFilesFor(ExtensionRequest extensionRequest, List<IFormFile> formFiles);

        void SaveExtensionRequest(ExtensionRequest extensionRequest);
    }
}