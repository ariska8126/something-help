using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FileCopyApps.Entities;

public partial class RnDContext : DbContext
{
    public RnDContext()
    {
    }

    public RnDContext(DbContextOptions<RnDContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TableRcsCompleteDatum> TableRcsCompleteData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TableRcsCompleteDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Table_RCS_complete_data");

            entity.Property(e => e.Amount).HasColumnName("AMOUNT");
            entity.Property(e => e.Bin)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("bin");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Deskripsi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("deskripsi");
            entity.Property(e => e.Deskripsi1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("deskripsi1");
            entity.Property(e => e.DiscAmt).HasColumnName("DISC_AMT");
            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.NamaCabang)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nama_Cabang");
            entity.Property(e => e.New1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.No)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NoMerchant)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("No_Merchant");
            entity.Property(e => e.Oh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("OH");
            entity.Property(e => e.Op)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("OP");
            entity.Property(e => e.Rate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("RATE");
            entity.Property(e => e.Rek)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rek");
            entity.Property(e => e.Tanggal)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tanggal_");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
