using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MitigatingCircumstances.Services.Interface
{
    public interface ICloudStorageService
    {
        Google.Apis.Storage.v1.Data.Object UploadFormFile(IFormFile formFile);

        List<Google.Apis.Storage.v1.Data.Object> UploadFormFiles(List<IFormFile> formFiles);
    }
}