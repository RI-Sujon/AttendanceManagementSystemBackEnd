using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RifatSirProjectAPI5.Models
{
    public class DatabaseContext : DbContext
    {
        //public AuthenticationContext(DbContext option)
        public const string ConnectionString = @"Server=LAPTOP-COS90VRD\SQLEXPRESS; Database=WebTechProject ; Trusted_Connection=true";

        public DbSet<StudentAuth> StudentAuthTable { get; set; }
        public DbSet<StudentBasicInfo> StudentBasicInfoTable { get; set; }
        public DbSet<AdminAuth> AdminAuthTable { get; set; }
        public DbSet<AdminBasicInfo> AdminBasicInfoTable { get; set; }
        public DbSet<CourseBasicInfo> CourseBasicInfoTable { get; set; }
        public DbSet<CourseStudent> CourseStudentRelationTable { get; set; }
        public DbSet<CoursePosts> CoursePostsTable { get; set; }
        public DbSet<Attendance> AttendanceTable { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
