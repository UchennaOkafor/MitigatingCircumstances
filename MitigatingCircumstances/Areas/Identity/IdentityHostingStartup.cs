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
                    options.UseMySql(context.Configuration.GetConnectionString("GoogleCloudSql")));

                services.AddDefaultIdentity<AppUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

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