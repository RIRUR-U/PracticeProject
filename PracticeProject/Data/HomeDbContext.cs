using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PracticeProject.Data.Entity;

namespace PracticeProject.Data
{
    public partial class HomeDbContext : DbContext
    {
        public HomeDbContext()
        {
        }

        public HomeDbContext(DbContextOptions<HomeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblItem> TblItems { get; set; } = null!;
        public virtual DbSet<TblPartner> TblPartners { get; set; } = null!;
        public virtual DbSet<TblPartnerType> TblPartnerTypes { get; set; } = null!;
        public virtual DbSet<TblPurchase> TblPurchases { get; set; } = null!;
        public virtual DbSet<TblPurchaseDetail> TblPurchaseDetails { get; set; } = null!;
        public virtual DbSet<TblSale> TblSales { get; set; } = null!;
        public virtual DbSet<TblSalesDetail> TblSalesDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-60K79Q8\\SQLEXPRESS;Database= practiceProj;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblItem>(entity =>
            {
                entity.HasKey(e => e.IntItemId)
                    .HasName("PK__tblItem__FA6F1B1217065F8C");

                entity.ToTable("tblItem");

                entity.Property(e => e.IntItemId).HasColumnName("intItemId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NumStockQuantity).HasColumnName("numStockQuantity");

                entity.Property(e => e.StrItemName)
                    .HasMaxLength(250)
                    .HasColumnName("strItemName");
            });

            modelBuilder.Entity<TblPartner>(entity =>
            {
                entity.HasKey(e => e.IntPartnerId)
                    .HasName("PK__tblPartn__279F3038BC31A55A");

                entity.ToTable("tblPartner");

                entity.Property(e => e.IntPartnerId).HasColumnName("intPartnerId");

                entity.Property(e => e.IntPartnerTypeId).HasColumnName("intPartnerTypeId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StrPartnerName)
                    .HasMaxLength(250)
                    .HasColumnName("strPartnerName");
            });

            modelBuilder.Entity<TblPartnerType>(entity =>
            {
                entity.HasKey(e => e.IntPartnerTypeId)
                    .HasName("PK__tblPartn__35301953EB6253B9");

                entity.ToTable("tblPartnerType");

                entity.Property(e => e.IntPartnerTypeId).HasColumnName("intPartnerTypeId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StrPartnerTypeName)
                    .HasMaxLength(250)
                    .HasColumnName("strPartnerTypeName");
            });

            modelBuilder.Entity<TblPurchase>(entity =>
            {
                entity.HasKey(e => e.IntPurchaseId)
                    .HasName("PK__tblPurch__39AFE60534FED946");

                entity.ToTable("tblPurchase");

                entity.Property(e => e.IntPurchaseId).HasColumnName("intPurchaseId");

                entity.Property(e => e.DtePurchaseDate)
                    .HasColumnType("date")
                    .HasColumnName("dtePurchaseDate");

                entity.Property(e => e.IntSupplierId).HasColumnName("intSupplierId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblPurchaseDetail>(entity =>
            {
                entity.HasKey(e => e.IntDetailsId)
                    .HasName("PK__tblPurch__0A1B5AF3025013EE");

                entity.ToTable("tblPurchaseDetails");

                entity.Property(e => e.IntDetailsId).HasColumnName("intDetailsId");

                entity.Property(e => e.IntItemId).HasColumnName("intItemId");

                entity.Property(e => e.IntPurchaseId).HasColumnName("intPurchaseId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NumItemQuantity).HasColumnName("numItemQuantity");

                entity.Property(e => e.NumUnitPrice).HasColumnName("numUnitPrice");
            });

            modelBuilder.Entity<TblSale>(entity =>
            {
                entity.HasKey(e => e.IntSalesId)
                    .HasName("PK__tblSales__754F6C55DE1A411D");

                entity.ToTable("tblSales");

                entity.Property(e => e.IntSalesId).HasColumnName("intSalesId");

                entity.Property(e => e.DteSalesDate)
                    .HasColumnType("date")
                    .HasColumnName("dteSalesDate");

                entity.Property(e => e.IntCustomerId).HasColumnName("intCustomerId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblSalesDetail>(entity =>
            {
                entity.HasKey(e => e.IntDetailsId)
                    .HasName("PK__tblSales__0A1B5AF355B140F9");

                entity.ToTable("tblSalesDetails");

                entity.Property(e => e.IntDetailsId).HasColumnName("intDetailsId");

                entity.Property(e => e.IntItemId).HasColumnName("intItemId");

                entity.Property(e => e.IntItemQuantity).HasColumnName("intItemQuantity");

                entity.Property(e => e.IntSalesId).HasColumnName("intSalesId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NumUnitPrice).HasColumnName("numUnitPrice");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
