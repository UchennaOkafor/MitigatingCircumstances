using Google.Cloud.Datastore.V1;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MitigatingCircumstances.Repositories;
using System;

namespace MitigatingCircumstances.DependencyInjection
{
    public static class ServicesConfigurator
    {
        private const string EmulatorHostVariable = "DATASTORE_EMULATOR_HOST";
        private const string ProjectIdVariable = "DATASTORE_PROJECT_ID";

        /// <summary>
        /// Creates a <see cref="DatastoreDb"/>, using environment variables to support
        /// the Datastore Emulator if they have been set.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the DATASTORE_EMULATOR_HOST environment variable is set and not empty,
        /// this method will use the value to connect to the emulator. Otherwise, the default
        /// endpoint (and credentials) will be used.
        /// </para>
        /// <para>
        /// If the DATASTORE_PROJECT_ID environment variable is set and not empty, this
        /// overrides the value of <paramref name="projectId"/>.
        /// </para>
        /// </remarks>
        /// <param name="projectId">The project ID to use, unless overridden by DATASTORE_PROJECT_ID</param>
        /// <param name="namespaceId">The namespace ID to use in operations requiring a partition.</param>
        /// <returns>A <see cref="DatastoreDb"/> connected to either an emulator or production servers.</returns>
        public static DatastoreDb CreateDatastoreDb(string projectId, string namespaceId = "")
        {
            string emulatorHost = Environment.GetEnvironmentVariable(EmulatorHostVariable, EnvironmentVariableTarget.User);
            string projectIdOverride = Environment.GetEnvironmentVariable(ProjectIdVariable, EnvironmentVariableTarget.User);

            if (!string.IsNullOrEmpty(projectIdOverride))
            {
                projectId = projectIdOverride;
            }
            DatastoreClient client = string.IsNullOrEmpty(emulatorHost)
                ? DatastoreClient.Create()
                : DatastoreClient.Create(new Channel(emulatorHost, ChannelCredentials.Insecure));
            return DatastoreDb.Create(projectId, namespaceId, client);
        }

        //TODO Rewrite this to include actual logic
        public static void AddServicesDependencies(this IServiceCollection services, IHostingEnvironment environment, string projectId)
        {
            var db = CreateDatastoreDb(projectId);

            //if (db.)
            if (environment.IsDevelopment())
            {
                services.AddSingleton(db);
            }
            else if (environment.IsProduction())
            {
                services.AddSingleton(db);
            }
            else
            {
                //Error
            }

            //services.AddSingleton(DatastoreDb.Create(projectId));
            services.AddTransient<ITicketRepository, StudentTicketRepository>();
        }
    }
}
