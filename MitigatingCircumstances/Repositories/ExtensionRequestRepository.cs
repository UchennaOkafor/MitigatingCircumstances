using Microsoft.AspNetCore.Http;
using MitigatingCircumstances.Models;
using MitigatingCircumstances.Models.Enum;
using MitigatingCircumstances.Repositories.Base;
using MitigatingCircumstances.Services.Interface;
using System.Collections.Generic;
using System.Linq;

namespace MitigatingCircumstances.Repositories
{
    public class ExtensionRequestRepository : BaseRepository, IExtensionRequestRepository
    {
        private readonly ICloudStorageService _cloudStorageService;

        public ExtensionRequestRepository(ApplicationDbContext dbContext, ICloudStorageService storageService) : base(dbContext)
        {
            _cloudStorageService = storageService;
        }

        public ExtensionRequest GetExtensionRequestById(int id)
        {
            return Context.ExtensionRequests.Find(id);
        }

        public IEnumerable<ExtensionRequest> GetExtensionRequestsAssignedTo(string tutorId)
        {
            return Context.ExtensionRequests.Where(er => er.TutorAssignedTo.Id == tutorId).OrderBy(er => er.Status);
        }

        public IEnumerable<ExtensionRequest> GetExtensionRequestsCreatedBy(string studentId)
        {
            return Context.ExtensionRequests.Where(er => er.StudentCreatedBy.Id == studentId).OrderBy(er => er.Status);
        }

        public void ChangeExtensionRequestState(ExtensionRequest extensionRequest, ExtensionRequestStatus newStatus)
        {
            extensionRequest.Status = newStatus;
            SaveChanges();
        }

        public List<UploadedDocument> UploadFilesFor(ExtensionRequest extensionRequest, List<IFormFile> formFiles)
        {
            var uploadedObjects = _cloudStorageService.UploadFormFiles(formFiles);
            var uploadedDocuments = new List<UploadedDocument>();

            foreach (var file in uploadedObjects)
            {
                uploadedDocuments.Add(new UploadedDocument
                {
                    CloudId = file.Id,
                    Bucket = file.Bucket,
                    MediaLink = file.MediaLink,
                    Name = file.Name,
                    ExtensionRequest = extensionRequest,
                    UploadedBy = extensionRequest.StudentCreatedBy
                });
            }

            return uploadedDocuments;
        }

        public void SaveExtensionRequest(ExtensionRequest extensionRequest)
        {
            Context.ExtensionRequests.Add(extensionRequest);
            SaveChanges();
        }
    }
}
