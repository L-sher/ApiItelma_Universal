using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal.KTLinesDBContexts.FirstLineWatchDog;

public partial class FirstLineWatchDogContext : DbContext
{
    public FirstLineWatchDogContext()
    {
    }

    public FirstLineWatchDogContext(DbContextOptions<FirstLineWatchDogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CheckPointsList> CheckPointsLists { get; set; }

    public virtual DbSet<PassedKt> PassedKts { get; set; }

    public virtual DbSet<ProductCheckPointSetting> ProductCheckPointSettings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\mssqlshermatov;Initial Catalog=FirstLineWatchDog;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CheckPointsList>(entity =>
        {
            entity.HasKey(e => e.CheckPointId);

            entity.ToTable("CheckPointsList");

            entity.Property(e => e.CheckPointIp).HasColumnName("CheckPointIP");
        });

        modelBuilder.Entity<PassedKt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ActualKT");

            entity.ToTable("PassedKT");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Barcode).HasMaxLength(25);
            entity.Property(e => e.NextKt)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("NextKT");
            entity.Property(e => e.PassedKt1).HasColumnName("PassedKT");

            entity.HasOne(d => d.PassedKt1Navigation).WithMany(p => p.PassedKts)
                .HasForeignKey(d => d.PassedKt1)
                .HasConstraintName("FK_ActualKT_CheckPointsList");
        });

        modelBuilder.Entity<ProductCheckPointSetting>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.ProductCheckPointSettings).HasMaxLength(30);
            entity.Property(e => e.ProductName).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
