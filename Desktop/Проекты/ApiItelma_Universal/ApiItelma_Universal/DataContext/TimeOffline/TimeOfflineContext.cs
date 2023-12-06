using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal.DBContext.TimeOffline;

public partial class TimeOfflineContext : DbContext
{
    public TimeOfflineContext()
    {
    }

    public TimeOfflineContext(DbContextOptions<TimeOfflineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllDevice> AllDevices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=N139\\MSSQLSHERMATOV;Initial Catalog=TimeOffline;TrustServerCertificate=True;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllDevice>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeviceName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.IpAddress)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("ipAddress");
            entity.Property(e => e.OfflineStatusDate)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
