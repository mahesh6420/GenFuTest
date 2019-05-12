using System;
using GenFuTest.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GenFuTest.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountType> AccountType { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Loan> Loan { get; set; }
        public virtual DbSet<LoanAmount> LoanAmount { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //             if (!optionsBuilder.IsConfigured)
            //             {
            // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //                 optionsBuilder.UseSqlite("DataSource=app.db");
            //             }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.AccountTypeId);

                entity.HasIndex(e => e.CustomerId);

                entity.Property(e => e.Id).IsRequired();

                entity.Property(e => e.AccountNumber).IsRequired();

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.AccountTypeId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).IsRequired();

                entity.Property(e => e.City).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Street).IsRequired();
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<LoanAmount>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.LoanId);

                entity.Property(e => e.Id).IsRequired();

                entity.Property(e => e.LoanNumber).IsRequired();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.LoanAmount)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.LoanAmount)
                    .HasForeignKey(d => d.LoanId);
            });

        }
    }
}
