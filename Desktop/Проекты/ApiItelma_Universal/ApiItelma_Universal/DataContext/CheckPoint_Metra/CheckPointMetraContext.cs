using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal.DBContext.CheckPoint_Metra;

public partial class CheckPointMetraContext : DbContext
{
    public CheckPointMetraContext()
    {
    }

    public CheckPointMetraContext(DbContextOptions<CheckPointMetraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CheckPointDataPrinter> CheckPointDataPrinters { get; set; }

    public virtual DbSet<CheckPointDataProgrammer> CheckPointDataProgrammers { get; set; }

    public virtual DbSet<CheckPointDataUfk1> CheckPointDataUfk1s { get; set; }

    public virtual DbSet<CheckPointDataUfk3> CheckPointDataUfk3s { get; set; }

    public virtual DbSet<CheckPointData> CheckPointData { get; set; }

    public virtual DbSet<CheckPointSetting> CheckPointSettings { get; set; }

    public virtual DbSet<CheckPointSettingsUfk1> CheckPointSettingsUfk1s { get; set; }

    public virtual DbSet<CheckPointSettingsUfk3> CheckPointSettingsUfk3s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress03;Initial Catalog=CheckPoint_Metra;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CheckPointDataPrinter>(entity =>
        {
            entity.ToTable("CheckPointDataPrinter");

            entity.Property(e => e.LocalBarCode).HasMaxLength(400);
            entity.Property(e => e.Time).HasColumnType("datetime");

            entity.HasOne(d => d.LocalBarCodeNavigation).WithMany(p => p.CheckPointDataPrinters)
                .HasForeignKey(d => d.LocalBarCode)
                .HasConstraintName("FK_CheckPointDataPrinter_CheckPointData");
        });

        modelBuilder.Entity<CheckPointDataProgrammer>(entity =>
        {
            entity.ToTable("CheckPointDataProgrammer");

            entity.Property(e => e.LocalBarCode).HasMaxLength(400);
            entity.Property(e => e.TimeStart).HasColumnType("datetime");
            entity.Property(e => e.TimeStop).HasColumnType("datetime");

            entity.HasOne(d => d.LocalBarCodeNavigation).WithMany(p => p.CheckPointDataProgrammers)
                .HasForeignKey(d => d.LocalBarCode)
                .HasConstraintName("FK_CheckPointDataProgrammer_CheckPointData");
        });

        modelBuilder.Entity<CheckPointDataUfk1>(entity =>
        {
            entity.ToTable("CheckPointDataUfk1");

            entity.Property(e => e.Jsontest).HasColumnName("JSONTest");
            entity.Property(e => e.LocalBarCode).HasMaxLength(400);
            entity.Property(e => e.TimeStart).HasColumnType("datetime");
            entity.Property(e => e.TimeStop).HasColumnType("datetime");
            entity.Property(e => e.Vch).HasColumnName("VCh");

            entity.HasOne(d => d.LocalBarCodeNavigation).WithMany(p => p.CheckPointDataUfk1s)
                .HasForeignKey(d => d.LocalBarCode)
                .HasConstraintName("FK_CheckPointDataUfk1_CheckPointData");
        });

        modelBuilder.Entity<CheckPointDataUfk3>(entity =>
        {
            entity.ToTable("CheckPointDataUfk3");

            entity.Property(e => e.Iccid).HasColumnName("ICCID");
            entity.Property(e => e.Jsontest).HasColumnName("JSONTest");
            entity.Property(e => e.LocalBarCode).HasMaxLength(400);
            entity.Property(e => e.TimeStart).HasColumnType("datetime");
            entity.Property(e => e.TimeStop).HasColumnType("datetime");

            entity.HasOne(d => d.LocalBarCodeNavigation).WithMany(p => p.CheckPointDataUfk3s)
                .HasForeignKey(d => d.LocalBarCode)
                .HasConstraintName("FK_CheckPointDataUfk3_CheckPointData");
        });

        modelBuilder.Entity<CheckPointData>(entity =>
        {
            entity.HasKey(e => e.BarCode);

            entity.Property(e => e.BarCode).HasMaxLength(400);
            entity.Property(e => e.TimeCheck).HasColumnType("datetime");
        });

        modelBuilder.Entity<CheckPointSetting>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.KodUfk3Mts).HasColumnName("KodUfk3MTS");
        });

        modelBuilder.Entity<CheckPointSettingsUfk1>(entity =>
        {
            entity.ToTable("CheckPointSettingsUfk1");

            entity.HasOne(d => d.Product).WithMany(p => p.CheckPointSettingsUfk1s)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CheckPointSettingsUfk1_CheckPointSettings");
        });

        modelBuilder.Entity<CheckPointSettingsUfk3>(entity =>
        {
            entity.ToTable("CheckPointSettingsUfk3");

            entity.HasOne(d => d.Product).WithMany(p => p.CheckPointSettingsUfk3s)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CheckPointSettingsUfk3_CheckPointSettings");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
