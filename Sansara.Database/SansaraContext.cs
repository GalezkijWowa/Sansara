using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sansara.Database.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sansara.Database
{
    public class SansaraContext : DbContext
    {
        private IConfigurationRoot Configuration { get; set; }

        public SansaraContext()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("database.json", optional: false, reloadOnChange: true);
            this.Configuration = builder.Build();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = Configuration["ConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
