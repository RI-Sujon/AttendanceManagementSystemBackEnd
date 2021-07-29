using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RifatSirProjectAPI5.Areas.Identity.Data;

namespace RifatSirProjectAPI5.Data
{
    public class RifatSirProjectAPI5Context : IdentityDbContext
    {
        public DbSet<RifatSirProjectAPI5User> rifatSirProjectAPI5User { get; set; }
        public DbSet<RifatSirProjectAPI5Role> rifatSirProjectAPI5Role { get; set; }
        public DbSet<RifatSirProjectAPI5UserRole> rifatSirProjectAPI5UserRole { get; set; } 

        public RifatSirProjectAPI5Context(DbContextOptions<RifatSirProjectAPI5Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
