using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MitigatingCircumstances.Models;

[assembly: HostingStartup(typeof(MitigatingCircumstances.Areas.Identity.IdentityHostingStartup))]
namespace MitigatingCircumstances.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseLazyLoadingProxies();

                    if (context.HostingEnvironment.IsProduction() ||context.HostingEnvironment.IsStaging())
                    {
                        options.UseMySql(context.Configuration.GetConnectionString("GoogleCloudMySql"));
                    }
                    else
                    {
                        options.UseSqlServer(context.Configuration.GetConnectionString("LocalMsSqlServer"));
                    }
                });
                
                services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultUI();

                services.AddAuthentication().AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = context.Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = context.Configuration["Authentication:Google:ClientSecret"];
                });

                services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = context.Configuration["Authentication:Microsoft:ApplicationId"];
                    microsoftOptions.ClientSecret = context.Configuration["Authentication:Microsoft:Password"];
                });
            });
        }
    }
}