using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal.DBContext.ProductionLines;

public partial class ProductionLinesContext : DbContext
{
    public ProductionLinesContext()
    {
    }

    public ProductionLinesContext(DbContextOptions<ProductionLinesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LineSetting> LineSettings { get; set; }

    public virtual DbSet<ProcessesAndSetting> ProcessesAndSettings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=N139\\SQLEXPRESS03;Initial Catalog=ProductionLines;TrustServerCertificate=True;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LineSetting>(entity =>
        {
            entity.HasKey(e => e.LineId);

            entity.Property(e => e.LineProcess).HasMaxLength(200);
        });

        modelBuilder.Entity<ProcessesAndSetting>(entity =>
        {
            entity.Property(e => e.IsThereKtzero)
                .HasMaxLength(10)
                .HasColumnName("IsThereKTZero");
            entity.Property(e => e.ListKtinJson)
                .HasMaxLength(200)
                .HasColumnName("ListKTInJSON");
            entity.Property(e => e.ProcessName).HasMaxLength(100);
            entity.Property(e => e.ProductId).HasMaxLength(6);
            entity.Property(e => e.ProductRoute).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
