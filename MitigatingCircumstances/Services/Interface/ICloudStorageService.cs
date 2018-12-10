using Microsoft.AspNetCore.Http;

namespace MitigatingCircumstances.Services.Interface
{
    public interface ICloudStorageService
    {
        Google.Apis.Storage.v1.Data.Object UploadFormFile(IFormFile formFile);
    }
}