using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiItelma_Universal.DBContext.SettingSenderMan;

public partial class SettingSenderManContext : DbContext
{
    public SettingSenderManContext()
    {
    }

    public SettingSenderManContext(DbContextOptions<SettingSenderManContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClientMail> ClientMails { get; set; }

    public virtual DbSet<CopyMail> CopyMails { get; set; }

    public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

    public virtual DbSet<ProductId> ProductIds { get; set; }

    public virtual DbSet<SettingSenderManMail> SettingSenderManMails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress03;Initial Catalog=SettingSenderMan;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientMail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ClientMails");

            entity.HasIndex(e => e.SettingSenderManMailProgrammSend, "IX_SettingSenderManMail_ProgrammSend");

            entity.Property(e => e.SettingSenderManMailProgrammSend)
                .HasMaxLength(128)
                .HasColumnName("SettingSenderManMail_ProgrammSend");

            entity.HasOne(d => d.SettingSenderManMailProgrammSendNavigation).WithMany(p => p.ClientMails)
                .HasForeignKey(d => d.SettingSenderManMailProgrammSend)
                .HasConstraintName("FK_dbo.ClientMails_dbo.SettingSenderManMails_SettingSenderManMail_ProgrammSend");
        });

        modelBuilder.Entity<CopyMail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.CopyMails");

            entity.HasIndex(e => e.SettingSenderManMailProgrammSend, "IX_SettingSenderManMail_ProgrammSend");

            entity.Property(e => e.SettingSenderManMailProgrammSend)
                .HasMaxLength(128)
                .HasColumnName("SettingSenderManMail_ProgrammSend");

            entity.HasOne(d => d.SettingSenderManMailProgrammSendNavigation).WithMany(p => p.CopyMails)
                .HasForeignKey(d => d.SettingSenderManMailProgrammSend)
                .HasConstraintName("FK_dbo.CopyMails_dbo.SettingSenderManMails_SettingSenderManMail_ProgrammSend");
        });

        modelBuilder.Entity<MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");

            entity.ToTable("__MigrationHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ContextKey).HasMaxLength(300);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<ProductId>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ProductIds");

            entity.HasIndex(e => e.SettingSenderManMailProgrammSend, "IX_SettingSenderManMail_ProgrammSend");

            entity.Property(e => e.SettingSenderManMailProgrammSend)
                .HasMaxLength(128)
                .HasColumnName("SettingSenderManMail_ProgrammSend");

            entity.HasOne(d => d.SettingSenderManMailProgrammSendNavigation).WithMany(p => p.ProductIds)
                .HasForeignKey(d => d.SettingSenderManMailProgrammSend)
                .HasConstraintName("FK_dbo.ProductIds_dbo.SettingSenderManMails_SettingSenderManMail_ProgrammSend");
        });

        modelBuilder.Entity<SettingSenderManMail>(entity =>
        {
            entity.HasKey(e => e.ProgrammSend).HasName("PK_dbo.SettingSenderManMails");

            entity.Property(e => e.ProgrammSend).HasMaxLength(128);
            entity.Property(e => e.PreviosSend).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
