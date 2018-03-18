using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using sportex.api.domain;
using Microsoft.Extensions.Configuration;

namespace sportex.api.persistence
{
    public class Context : DbContext
    {
        IConfigurationRoot configuration;
        public Context(DbContextOptions<Context> options) : base(options)
        { }
        public Context() : base()
        {        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
                string connectionString = configuration.GetValue<string>("connectionStrings:LocalDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
            catch(Exception ex)
            {
                throw new Exception("Error en la conexión con la base de datos: " + ex.Message);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                //modelBuilder.Entity<Relationship>().HasOne(r => r.Profile1).WithOne().HasForeignKey<Relationship>(r => r.Profile2);
                //modelBuilder.Entity<Relationship>().HasOne(r => r.Profile2).WithOne().HasForeignKey<Relationship>(r => r.Profile1);
                modelBuilder.Entity<Relationship>().HasKey(r => new { r.ID, r.IdProfile1, r.IdProfile2 });

                modelBuilder.Entity<Relationship>().HasOne(x => x.Profile1).WithMany(y => y.Relationships1).HasForeignKey(x => x.IdProfile1).OnDelete(DeleteBehavior.Restrict);
                modelBuilder.Entity<Relationship>().HasOne(x => x.Profile2).WithMany(y => y.Relationships2).HasForeignKey(x => x.IdProfile2);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexión con la base de datos: " + ex.Message);
    }
}

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AdminProfile> AdminProfiles { get; set; }
        public DbSet<StandardProfile> StandardProfiles { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
    }
}
