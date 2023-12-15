using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FileCopyApps.Entities;

public partial class ConverterContext : DbContext
{
    public ConverterContext()
    {
    }

    public ConverterContext(DbContextOptions<ConverterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lookup> Lookups { get; set; }

    public virtual DbSet<ReportDatum> ReportData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ADVERS-65804-1; Initial Catalog=converter; User Id=sa;password=s3cur3;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<Lookup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Lookup");

            entity.ToTable("Lookup");

            entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdatedById).HasColumnName("UpdatedBy_Id");
            entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<ReportDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("report_data");

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.FileId).HasColumnName("FileID");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.LastTran).HasMaxLength(20);
            entity.Property(e => e.Nwk)
                .HasMaxLength(50)
                .HasColumnName("NWK");
            entity.Property(e => e.Pb)
                .HasMaxLength(50)
                .HasColumnName("PB");
            entity.Property(e => e.Pbasli)
                .HasMaxLength(50)
                .HasColumnName("PBAsli");
            entity.Property(e => e.TranDate).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
