using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal.DBContext.LabelSetting;

public partial class LabelSettingContext : DbContext
{
    public LabelSettingContext()
    {
    }

    public LabelSettingContext(DbContextOptions<LabelSettingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<PackageSetting> PackageSettings { get; set; }

    public virtual DbSet<PassportSetting> PassportSettings { get; set; }

    public virtual DbSet<ProductSetting> ProductSettings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=N139\\SQLEXPRESS03;Initial Catalog=LabelSetting;TrustServerCertificate=True;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Label>(entity =>
        {
            entity.HasKey(e => e.ProductId);
        });

        modelBuilder.Entity<PackageSetting>(entity =>
        {
            entity.HasIndex(e => e.LabelProductId, "IX_PackageSettings_LabelProductId");

            //entity.HasOne(d => d.LabelProduct).WithMany(p => p.PackageSettings).HasForeignKey(d => d.LabelProductId);
        });

        modelBuilder.Entity<PassportSetting>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.LabelProductId).HasMaxLength(450);

            //entity.HasOne(d => d.LabelProduct).WithMany(p => p.PassportSettings)
            //    .HasForeignKey(d => d.LabelProductId)
            //    .HasConstraintName("FK_PassportSettings_Labels");
        });

        modelBuilder.Entity<ProductSetting>(entity =>
        {
            entity.HasIndex(e => e.LabelProductId, "IX_ProductSettings_LabelProductId");

            //entity.HasOne(d => d.LabelProduct).WithMany(p => p.ProductSettings).HasForeignKey(d => d.LabelProductId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
