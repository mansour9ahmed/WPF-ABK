using Constraints;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models
{
    public class BurakDbContext : DbContext
    {
        public BurakDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new CompanyConstraint(builder.Entity<Company>());
            new CompanyEmailConstraint(builder.Entity<CompanyEmail>());
            new InvoiceConstraint(builder.Entity<Invoice>());
            new SoaConstraint(builder.Entity<Soa>());
            new SoaElementConstraint(builder.Entity<SoaElement>());
            new VesselConstraint(builder.Entity<Vessel>());
            new InvoiceSoaElementConstraint(builder.Entity<InvoiceSoaElement>());

            builder.Entity<BankAccount>().HasMany(b => b.Invoices).WithOne(i => i.Bank).OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyEmail> CompanyEmails { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Soa> Soas { get; set; }
        public DbSet<SoaElement> SoaElements { get; set; }
        public DbSet<Vessel> Vessels { get; set; }
        public DbSet<InvoiceSoaElement> InvoiceSoaElements { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<TransactionAccount> TransactionAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
