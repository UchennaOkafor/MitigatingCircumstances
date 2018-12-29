using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using MitigatingCircumstances.Services.Interface;
using System.Collections.Generic;
using System.IO;

namespace MitigatingCircumstances.Services
{
    public class GoogleCloudStorageService : ICloudStorageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _storageBucketName;
        private readonly string _tempFilePath;

        public GoogleCloudStorageService()
        {
            _storageClient = StorageClient.Create();
            _storageBucketName = "future-system-219911.appspot.com";
            _tempFilePath = Path.GetTempFileName();
        }

        public Google.Apis.Storage.v1.Data.Object UploadFormFile(IFormFile formFile)
        {
            if (formFile.Length <= 0)
            {
                return null;
            }

            using (var stream = new FileStream(_tempFilePath, FileMode.Create))
            {
                formFile.CopyTo(stream);

                return _storageClient.UploadObject(_storageBucketName, 
                    formFile.FileName, formFile.ContentType, stream);
            }
        }

        public List<Google.Apis.Storage.v1.Data.Object> UploadFormFiles(List<IFormFile> formFiles)
        {
            if (formFiles.Count == 0)
            {
                return null;
            }

            var uploadedObjects = new List<Google.Apis.Storage.v1.Data.Object>();

            foreach (var formFile in formFiles)
            {
                uploadedObjects.Add(UploadFormFile(formFile));
            }

            return uploadedObjects;
        }
    }
}
