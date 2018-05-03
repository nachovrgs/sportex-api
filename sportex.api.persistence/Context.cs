using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using sportex.api.domain;
using Microsoft.Extensions.Configuration;
using sportex.api.domain.notification;

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
                //for Relationships
                modelBuilder.Entity<Relationship>().HasKey(r => new { r.IdProfile1, r.IdProfile2 });
                modelBuilder.Entity<Relationship>().HasOne(x => x.Profile1).WithMany(y => y.Relationships1).HasForeignKey(x => x.IdProfile1).OnDelete(DeleteBehavior.Restrict);
                modelBuilder.Entity<Relationship>().HasOne(x => x.Profile2).WithMany(y => y.Relationships2).HasForeignKey(x => x.IdProfile2);


                //for EventParticipants
                modelBuilder.Entity<EventParticipant>().HasKey(ep => new { ep.EventID, ep.StandardProfileID });
                modelBuilder.Entity<EventParticipant>().HasOne(x => x.EventParticipates).WithMany(y => y.EventParticipates).HasForeignKey(x => x.EventID).OnDelete(DeleteBehavior.Restrict);
                modelBuilder.Entity<EventParticipant>().HasOne(x => x.ProfileParticipant).WithMany(y => y.ProfileParticipant).HasForeignKey(x => x.StandardProfileID);

                //for EventInvitations
                modelBuilder.Entity<EventInvitation>().HasKey(ei => new { ei.EventID, ei.IdProfileInvites,  ei.ProfileInvited});
                modelBuilder.Entity<EventInvitation>().HasOne(x => x.EventInvited).WithMany(y => y.EventInvited).HasForeignKey(x => x.EventID).OnDelete(DeleteBehavior.Restrict);
                modelBuilder.Entity<EventInvitation>().HasOne(x => x.ProfileInvites).WithMany(y => y.ProfileInvites).HasForeignKey(x => x.IdProfileInvites);
                modelBuilder.Entity<EventInvitation>().HasOne(x => x.ProfileInvited).WithMany(y => y.ProfileInvited).HasForeignKey(x => x.IdProfileInvited);
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
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<EventInvitation> EventInvitations { get; set; }
    }
}
