using Google.Cloud.Datastore.V1;
using Microsoft.Extensions.DependencyInjection;
using MitigatingCircumstances.Repositories;
using MitigatingCircumstances.Repositories.Interface;
using MitigatingCircumstances.Services;
using MitigatingCircumstances.Services.Interface;

namespace MitigatingCircumstances.DependencyInjection
{
    public static class ServicesConfigurator
    {
        public static void AddServicesDependencies(this IServiceCollection services, string projectId)
        {
            services.AddSingleton(DatastoreDb.Create(projectId));
            services.AddTransient<IBaseRepository, BaseRepository>();
            services.AddTransient<IExtensionRequestRepository, ExtensionRequestRepository>();
            services.AddTransient<ICloudStorageService, GoogleCloudStorageService>();
            services.AddTransient<IMailService, SendGridMailService>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
        }
    }
}
