using System.Reflection;
using INVCAPP.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace INVCAPP.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=NTBK-0029;Initial Catalog=INVCAPP;Integrated Security=True;TrustServerCertificate=True;");
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-UELITFO\SQLEXPRESS;Initial Catalog=INVCAPP;Integrated Security=True;TrustServerCertificate=True;");

        }

    }
}