using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal.DBContext.Permissions;

public partial class PermissionsContext : DbContext
{
    public PermissionsContext()
    {
    }

    public PermissionsContext(DbContextOptions<PermissionsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeesList> EmployeesLists { get; set; }

    public virtual DbSet<PermissionRule> PermissionRules { get; set; }

    public virtual DbSet<PermissionsList> PermissionsLists { get; set; }

    public virtual DbSet<WebSitePagesList> WebSitePagesLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress03;Initial Catalog=Permissions;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeesList>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("EmployeesList");
        });

        modelBuilder.Entity<PermissionRule>(entity =>
        {
            entity.HasKey(e => e.RuleId);

            entity.Property(e => e.CheckPointId).HasDefaultValueSql("((0))");
            entity.Property(e => e.PermissionId).HasDefaultValueSql("((2))");

            entity.HasOne(d => d.Employee).WithMany(p => p.PermissionRules)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermissionRules_EmployeesList");

            entity.HasOne(d => d.Permission).WithMany(p => p.PermissionRules)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermissionRules_PermissionsList");

            entity.HasOne(d => d.WebSitePage).WithMany(p => p.PermissionRules)
                .HasForeignKey(d => d.WebSitePageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermissionRules_WebSitePagesList");
        });

        modelBuilder.Entity<PermissionsList>(entity =>
        {
            entity.HasKey(e => e.PermissionId);

            entity.ToTable("PermissionsList");
        });

        modelBuilder.Entity<WebSitePagesList>(entity =>
        {
            entity.HasKey(e => e.WebSitePageId);

            entity.ToTable("WebSitePagesList");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
