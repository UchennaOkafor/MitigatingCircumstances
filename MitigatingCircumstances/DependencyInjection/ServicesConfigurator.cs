using Google.Cloud.Datastore.V1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MitigatingCircumstances.Repositories;

namespace MitigatingCircumstances.DependencyInjection
{
    public static class ServicesConfigurator
    {
        public static void AddServicesDependencies(this IServiceCollection services, string projectId)
        {
            services.AddSingleton(DatastoreDb.Create(projectId));
            services.AddTransient<IStudentRequestRepository, StudentRequestRepository>();
        }
    }
}
