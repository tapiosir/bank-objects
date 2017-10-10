﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClassLibrary1.Models
{
    public partial class BankdbContext : DbContext
    {
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<BankAccount> BankAccount { get; set; }
        public virtual DbSet<BankAccountTransaction> BankAccountTransaction { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=DKO-S010A-003\SQLEXPRESS;Initial Catalog=bankdb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.Property(e => e.Iban).ValueGeneratedNever();

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.BankAccount)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccount_Bank");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BankAccount)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccount_Customer");
            });

            modelBuilder.Entity<BankAccountTransaction>(entity =>
            {
                entity.HasOne(d => d.IbanNavigation)
                    .WithMany(p => p.BankAccountTransaction)
                    .HasForeignKey(d => d.Iban)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountTransaction_BankAccount");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Bank");
            });
        }
    }
}
