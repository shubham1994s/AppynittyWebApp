using System;
using AppynittyWebApp.Areas.Identity.Data;
using AppynittyWebApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AppynittyWebApp.Areas.Identity.IdentityHostingStartup))]
namespace AppynittyWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AppynittyWebAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AppynittyWebAppContextConnection")));

                services.AddDefaultIdentity<AppynittyWebAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AppynittyWebAppContext>();
            });
        }
    }
}