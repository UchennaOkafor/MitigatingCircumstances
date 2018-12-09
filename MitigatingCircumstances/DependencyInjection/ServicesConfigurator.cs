﻿using Google.Cloud.Datastore.V1;
using Microsoft.Extensions.DependencyInjection;
using MitigatingCircumstances.Repositories;
using MitigatingCircumstances.Repositories.Base;
using MitigatingCircumstances.Repositories.Interface;

namespace MitigatingCircumstances.DependencyInjection
{
    public static class ServicesConfigurator
    {
        public static void AddServicesDependencies(this IServiceCollection services, string projectId)
        {
            services.AddSingleton(DatastoreDb.Create(projectId));
            services.AddTransient<ITicketRepository, StudentTicketRepository>();
            services.AddTransient<IBaseRepository, BaseRepository>();
            services.AddTransient<ISupportTicketRepository, SupportTicketRepository>();
        }
    }
}
