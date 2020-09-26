using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Flow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Identity.Core;
using Microsoft.Extensions.Options;
using Flow.Models.Logs;

namespace Flow.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ROLE_ID = ADMIN_ID;
            builder.Entity<IdentityRole>().HasData(
            new IdentityRole
                {
                Id = ROLE_ID,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
                },
    
            new IdentityRole
                {
                Name = "Supervisor",
                NormalizedName = "SUPERVISOR"
                },
            new IdentityRole
                {
                Name = "QA",
                NormalizedName = "QA"
                },
            new IdentityRole
                {
                Name = "MfgEngineer",
                NormalizedName = "MFGENGINEER"
                },
            new IdentityRole
                {
                Name = "Operator",
                NormalizedName = "OPERATOR"
                }
            );

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin@company.com",
                NormalizedUserName = "ADMIN@COMPANY.COM",
                Email = "admin@company.com",
                NormalizedEmail = "admin@company.com",
                EmailConfirmed = true,
                UserRole = "Administrator",
                PasswordHash = "AQAAAAEAACcQAAAAEMeAin6TChHcMUl7NqSrLtNvCxK/ih/DMBlA0thAy6MWsmlIgRk1gvyFPqPOVgNiLA==",
                SecurityStamp = string.Empty
            });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
        public DbSet<Department> Department { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitLog> UnitLogs { get; set; }
        public DbSet<Workstation> Workstations { get; set; }
    }
}
