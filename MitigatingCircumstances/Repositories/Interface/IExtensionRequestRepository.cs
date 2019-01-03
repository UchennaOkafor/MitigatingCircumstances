using Microsoft.AspNetCore.Http;
using MitigatingCircumstances.Models;
using MitigatingCircumstances.Models.Enum;
using System.Collections.Generic;

namespace MitigatingCircumstances.Repositories.Interface
{
    public interface IExtensionRequestRepository
    {
        ExtensionRequest GetExtensionRequestById(int id);

        IEnumerable<ExtensionRequest> GetExtensionRequestsAssignedTo(string tutorId);

        IEnumerable<ExtensionRequest> GetExtensionRequestsCreatedBy(string studentId);

        void ChangeExtensionRequestState(ExtensionRequest extensionRequest, ExtensionRequestStatus newStatus);

        List<UploadedDocument> UploadFilesFor(ExtensionRequest extensionRequest, List<IFormFile> formFiles);

        void SaveExtensionRequest(ExtensionRequest extensionRequest);
    }
}