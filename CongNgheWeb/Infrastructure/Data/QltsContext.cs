using System;
using System.Collections.Generic;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class QltsContext : DbContext
{
    public QltsContext()
    {
    }

    public QltsContext(DbContextOptions<QltsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaoTri> BaoTris { get; set; }

    public virtual DbSet<LoaiTaiSan> LoaiTaiSans { get; set; }


    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<PhanBoTaiSan> PhanBoTaiSans { get; set; }

    public virtual DbSet<Phong> Phongs { get; set; }

    public virtual DbSet<TaiSan> TaiSans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Initial Catalog=QLTS;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaoTri>(entity =>
        {
            entity.HasKey(e => e.MaBaoTri).HasName("PK__BaoTri__51A9CA515034167A");

            entity.ToTable("BaoTri");

            entity.Property(e => e.NguoiThucHien).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasMaxLength(100);

            entity.HasOne(d => d.MaPhanBoNavigation).WithMany(p => p.BaoTris)
                .HasForeignKey(d => d.MaPhanBo)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<LoaiTaiSan>(entity =>
        {
            entity.HasKey(e => e.MaLoaiTaiSan).HasName("PK__LoaiTaiS__DAFA3F3C8ED39349");

            entity.ToTable("LoaiTaiSan");

            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.TenLoaiTaiSan).HasMaxLength(255);
        });

        

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__NguoiDun__C539D762AAD215E9");

            entity.ToTable("NguoiDung");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.HoTen).HasMaxLength(255);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.VaiTro).HasMaxLength(100);
        });

        modelBuilder.Entity<PhanBoTaiSan>(entity =>
        {
            entity.HasKey(e => e.MaPhanBo).HasName("PK__PhanBoTa__6EEE06712F38711D");

            entity.ToTable("PhanBoTaiSan");

            entity.Property(e => e.SoLuongDangBaoTri).HasDefaultValue(0);
            entity.Property(e => e.TinhTrang).HasMaxLength(100);

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.PhanBoTaiSans)
                .HasForeignKey(d => d.MaPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhanBoTai__MaPho__3F466844");

            entity.HasOne(d => d.MaTaiSanNavigation).WithMany(p => p.PhanBoTaiSans)
                .HasForeignKey(d => d.MaTaiSan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhanBoTai__MaTai__3E52440B");
        });

        modelBuilder.Entity<Phong>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__Phong__20BD5E5BFDEF3980");

            entity.ToTable("Phong");

            entity.Property(e => e.TenPhong).HasMaxLength(255);
            entity.Property(e => e.ViTri).HasMaxLength(255);
        });

        modelBuilder.Entity<TaiSan>(entity =>
        {
            entity.HasKey(e => e.MaTaiSan).HasName("PK__TaiSan__8DB7C7BECE51DB73");

            entity.ToTable("TaiSan");

            entity.Property(e => e.GiaTri).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaQr)
                .HasMaxLength(500)
                .HasColumnName("MaQR");
            entity.Property(e => e.TenTaiSan).HasMaxLength(255);
            entity.Property(e => e.TinhTrang).HasMaxLength(100);

            entity.HasOne(d => d.MaLoaiTaiSanNavigation).WithMany(p => p.TaiSans)
                .HasForeignKey(d => d.MaLoaiTaiSan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiSan__MaLoaiTa__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
