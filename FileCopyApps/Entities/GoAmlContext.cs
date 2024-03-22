using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FileCopyApps.Entities {

public partial class GoAmlContext : DbContext
{
    public GoAmlContext()
    {
    }

    public GoAmlContext(DbContextOptions<GoAmlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lookup> Lookups { get; set; }

    public virtual DbSet<TableIconv> TableIconvs { get; set; }

    public virtual DbSet<TableRc> TableRcs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=192.168.174.83; initial catalog=GoAML;persist security info=True;user id=sa;password=BNI46sql;MultipleActiveResultSets=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lookup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Lookup");

            entity.ToTable("Lookup");

            entity.HasIndex(e => e.CreatedById, "IX_CreatedBy_Id");

            entity.HasIndex(e => e.UpdatedById, "IX_UpdatedBy_Id");

            entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
            entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");
            entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<TableIconv>(entity =>
        {
            entity.ToTable("Table_IConv");

            entity.Property(e => e.Id)
                .HasMaxLength(72)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(72)
                .IsUnicode(false)
                .HasColumnName("card_number");
            entity.Property(e => e.DiscAmount).HasColumnName("disc_amount");
            entity.Property(e => e.MdrType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mdr_type");
            entity.Property(e => e.Principal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("principal");
        });

        modelBuilder.Entity<TableRc>(entity =>
        {
            entity.ToTable("Table_RCS");

            entity.Property(e => e.Id)
                .HasMaxLength(72)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(72)
                .IsUnicode(false)
                .HasColumnName("card_number");
            entity.Property(e => e.DiscAmount).HasColumnName("disc_amount");
            entity.Property(e => e.MdrType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mdr_type");
            entity.Property(e => e.Principal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("principal");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
} }
