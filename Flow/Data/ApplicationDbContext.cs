using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Flow.Models;
using Microsoft.AspNetCore.Identity;

namespace Flow.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
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
        }
        public DbSet<Flow.Models.Plant> Plant { get; set; }
    }
}
