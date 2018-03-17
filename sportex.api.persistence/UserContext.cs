using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using sportex.api.domain;
using Microsoft.Extensions.Configuration;

namespace sportex.api.persistence
{
    public class UserContext : DbContext
    {
        IConfigurationRoot configuration;
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }
        public UserContext() : base()
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

        public DbSet<User> Users { get; set; }
        public DbSet<AdminProfile> AdminProfiles { get; set; }
        public DbSet<StandardProfile> StandardProfiles { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
    }
}
