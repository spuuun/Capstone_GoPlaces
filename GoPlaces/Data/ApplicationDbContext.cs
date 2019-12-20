using System;
using System.Collections.Generic;
using System.Text;
using GoPlaces.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoPlaces.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Adventure> Adventures { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Place>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()"); 
            
            modelBuilder.Entity<Adventure>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}

