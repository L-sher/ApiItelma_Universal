using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal.DataContext.Traceability;

public partial class TraceabilityContext : DbContext
{
    public TraceabilityContext()
    {
    }

    public TraceabilityContext(DbContextOptions<TraceabilityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Global> Globals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=N139\\SQLEXPRESS03;Initial Catalog=Traceability;TrustServerCertificate=True;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Global>(entity =>
        {
            entity.HasKey(e => e.BarCode);

            entity.ToTable("Global");

            entity.Property(e => e.BarCode).HasMaxLength(400);
            entity.Property(e => e.CheckTime).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
