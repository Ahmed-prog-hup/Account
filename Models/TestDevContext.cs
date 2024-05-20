using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Account.Models;

public partial class TestDevContext : DbContext
{
    public TestDevContext()
    {

    }

    public TestDevContext(DbContextOptions<TestDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccNumber);

            entity.Property(e => e.AccNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Acc_Number");
            entity.Property(e => e.AccParent)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ACC_Parent");
            entity.Property(e => e.Balance).HasColumnType("decimal(20, 9)");

            entity.HasOne(d => d.AccParentNavigation).WithMany(p => p.InverseAccParentNavigation)
                .HasForeignKey(d => d.AccParent)
                .HasConstraintName("FK_Accounts_Accounts");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
