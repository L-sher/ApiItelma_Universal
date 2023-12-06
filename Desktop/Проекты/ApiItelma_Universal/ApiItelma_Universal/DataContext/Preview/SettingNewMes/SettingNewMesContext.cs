using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal.DBContext.Preview.SettingNewMes;

public partial class SettingNewMesContext : DbContext
{
    public SettingNewMesContext()
    {
    }

    public SettingNewMesContext(DbContextOptions<SettingNewMesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CheckStepsUfk1> CheckStepsUfk1s { get; set; }

    public virtual DbSet<CheckStepsUfk3> CheckStepsUfk3s { get; set; }

    public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=N139\\SQLEXPRESS03;Initial Catalog=SettingNewMes;TrustServerCertificate=True;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CheckStepsUfk1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.CheckStepsUfk1");

            entity.ToTable("CheckStepsUfk1");

            entity.HasIndex(e => e.SettingIdProduct, "IX_Setting_IdProduct");

            entity.Property(e => e.MaxVal).HasMaxLength(50);
            entity.Property(e => e.MinVal).HasMaxLength(50);
            entity.Property(e => e.SettingIdProduct)
                .HasMaxLength(128)
                .HasColumnName("Setting_IdProduct");

            entity.HasOne(d => d.SettingIdProductNavigation).WithMany(p => p.CheckStepsUfk1s)
                .HasForeignKey(d => d.SettingIdProduct)
                .HasConstraintName("FK_dbo.CheckStepsUfk1_dbo.Settings_Setting_IdProduct");
        });

        modelBuilder.Entity<CheckStepsUfk3>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.CheckStepsUfk3");

            entity.ToTable("CheckStepsUfk3");

            entity.HasIndex(e => e.SettingIdProduct, "IX_Setting_IdProduct");

            entity.Property(e => e.MaxVal).HasMaxLength(50);
            entity.Property(e => e.MinVal).HasMaxLength(50);
            entity.Property(e => e.SettingIdProduct)
                .HasMaxLength(128)
                .HasColumnName("Setting_IdProduct");

            entity.HasOne(d => d.SettingIdProductNavigation).WithMany(p => p.CheckStepsUfk3s)
                .HasForeignKey(d => d.SettingIdProduct)
                .HasConstraintName("FK_dbo.CheckStepsUfk3_dbo.Settings_Setting_IdProduct");
        });

        modelBuilder.Entity<MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");

            entity.ToTable("__MigrationHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ContextKey).HasMaxLength(300);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK_dbo.Settings");

            entity.Property(e => e.IdProduct).HasMaxLength(128);
            entity.Property(e => e.IsEasy).HasColumnName("isEasy");
            entity.Property(e => e.KodUfk3Mts).HasColumnName("KodUfk3MTS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
