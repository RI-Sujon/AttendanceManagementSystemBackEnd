using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RifatSirProjectAPI5.Areas.Identity.Data;
using RifatSirProjectAPI5.Data;

[assembly: HostingStartup(typeof(RifatSirProjectAPI5.Areas.Identity.IdentityHostingStartup))]
namespace RifatSirProjectAPI5.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RifatSirProjectAPI5Context>(options =>
                    options.UseSqlServer("Server=LAPTOP-COS90VRD\\SQLEXPRESS;Database=RifatSirProjectAPI5;Trusted_Connection=True;MultipleActiveResultSets=true"));

                services.AddDefaultIdentity<RifatSirProjectAPI5User>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<RifatSirProjectAPI5Role>()
                    .AddEntityFrameworkStores<RifatSirProjectAPI5Context>();
            });
        }
    }
}