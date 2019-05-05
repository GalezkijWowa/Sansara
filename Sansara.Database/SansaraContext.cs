using Microsoft.EntityFrameworkCore;
using Sansara.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sansara.Database
{
    public class SansaraContext : DbContext
    {
        //private IConfigurationRoot Configuration { get; set; }

        public SansaraContext()
        {
            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile("database.json", optional: false, reloadOnChange: true);
            //this.Configuration = builder.Build();
        }

        public DbSet<Artist> Artists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connectionString = Configuration["ConnectionString"];
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SansaraDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        }
    }
}
