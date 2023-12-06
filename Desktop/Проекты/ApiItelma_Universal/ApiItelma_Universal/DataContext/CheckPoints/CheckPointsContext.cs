using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal.DBContext.CheckPoints;

public partial class CheckPointsContext : DbContext
{
    public CheckPointsContext()
    {
    }

    public CheckPointsContext(DbContextOptions<CheckPointsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CameraList> CameraLists { get; set; }

    public virtual DbSet<CheckPointsList> CheckPointsLists { get; set; }

    public virtual DbSet<Plclist> Plclists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=N139\\SQLEXPRESS03;Initial Catalog=CheckPoints;TrustServerCertificate=True;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CameraList>(entity =>
        {
            entity.ToTable("CameraList");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CameraIpaddress)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CameraIPaddress");
            entity.Property(e => e.CameraName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CheckPointName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CheckPointsList>(entity =>
        {
            entity.HasKey(e => e.CheckPointId);

            entity.ToTable("CheckPointsList");

            entity.Property(e => e.CheckPointIp)
                .HasMaxLength(20)
                .HasColumnName("CheckPointIP");
            entity.Property(e => e.CheckPointLine)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.CheckPointName).HasMaxLength(30);
        });

        modelBuilder.Entity<Plclist>(entity =>
        {
            entity.ToTable("PLCList");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CheckPointName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Plcipaddress)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PLCIPAddress");
            entity.Property(e => e.Plcname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PLCName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
