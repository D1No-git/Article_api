using dll.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dll.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext() : base()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=D1NO;Database=articles;Trusted_Connection=true;TrustServerCertificate=true;");
            }
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}
