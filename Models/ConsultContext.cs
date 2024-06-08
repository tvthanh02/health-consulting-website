using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Models;

public partial class ConsultContext : DbContext
{
    public ConsultContext()
    {
    }

    public ConsultContext(DbContextOptions<ConsultContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BacSi> BacSis { get; set; }

    public virtual DbSet<BangThoiGianTuVan> BangThoiGianTuVans { get; set; }

    public virtual DbSet<ChuyenKhoa> ChuyenKhoas { get; set; }

    public virtual DbSet<DonViCongTac> DonViCongTacs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<Quyen> Quyens { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TrangThaiTuVan> TrangThaiTuVans { get; set; }

    public virtual DbSet<TuVan> TuVans { get; set; }

    public virtual DbSet<TuVanBangThoiGian> TuVanBangThoiGians { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=LAPTOP-TVT02\\MSSQL_2012;Initial Catalog=consult;User ID=;Password=;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BacSi>(entity =>
        {
            entity.HasKey(e => e.SMaBacSi).HasName("PK__BacSi__E470D536E8EE2067");

            entity.HasOne(d => d.SMaChuyenKhoaNavigation).WithMany(p => p.BacSis).HasConstraintName("FK__BacSi__sMaChuyen__1BFD2C07");

            entity.HasOne(d => d.SMaDonViCongTacNavigation).WithMany(p => p.BacSis).HasConstraintName("FK__BacSi__sMaDonViC__1DE57479");

            entity.HasOne(d => d.SMaTaiKhoanNavigation).WithMany(p => p.BacSis).HasConstraintName("FK__BacSi__sMaTaiKho__1CF15040");
        });

        modelBuilder.Entity<BangThoiGianTuVan>(entity =>
        {
            entity.HasKey(e => e.SMaThoiGianTuVan).HasName("PK__BangThoi__3FCAB332587359FD");
        });

        modelBuilder.Entity<ChuyenKhoa>(entity =>
        {
            entity.HasKey(e => e.SMaChuyenKhoa).HasName("PK__ChuyenKh__45D83D3432358F3B");
        });

        modelBuilder.Entity<DonViCongTac>(entity =>
        {
            entity.HasKey(e => e.SMaDonVi).HasName("PK__DonViCon__17898EE8908AF687");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.SMaNguoiDung).HasName("PK__NguoiDun__47E09BE3839AF2D6");

            entity.HasOne(d => d.SMaTaiKhoanNavigation).WithMany(p => p.NguoiDungs).HasConstraintName("FK__NguoiDung__sMaTa__15502E78");

        });

        modelBuilder.Entity<Quyen>(entity =>
        {
            entity.HasKey(e => e.SMaQuyen).HasName("PK__Quyen__0A7BEA5FD9F2B2CC");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SSessionId).HasName("PK__Session__138F9F35E2658B1B");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.SMaTaiKhoan).HasName("PK__TaiKhoan__0387828823E60E44");

            entity.HasOne(d => d.SMaQuyenNavigation).WithMany(p => p.TaiKhoans).HasConstraintName("FK__TaiKhoan__sMaQuy__1273C1CD");
        });

        modelBuilder.Entity<TrangThaiTuVan>(entity =>
        {
            entity.HasKey(e => e.SMaTrangThai).HasName("PK__TrangTha__C99ADF4E323444D8");
        });

        modelBuilder.Entity<TuVan>(entity =>
        {
            entity.HasKey(e => e.SMaLich).HasName("PK__TuVan__6D2B2A52143386AE");

            entity.HasOne(d => d.SMaBacSiNavigation).WithMany(p => p.TuVans).HasConstraintName("FK__TuVan__sMaBacSi__25869641");
        });

        modelBuilder.Entity<TuVanBangThoiGian>(entity =>
        {
            entity.HasKey(e => new { e.SMaLich, e.SMaThoiGianTuVan }).HasName("PK__TuVan_Ba__5ED7816151539FE3");

            entity.HasOne(d => d.SMaLichNavigation).WithMany(p => p.TuVanBangThoiGians)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TuVan_Ban__sMaLi__2A4B4B5E");

            entity.HasOne(d => d.SMaNguoiDungNavigation).WithMany(p => p.TuVanBangThoiGians).HasConstraintName("FK__TuVan_Ban__sMaNg__2B3F6F95");

            entity.HasOne(d => d.SMaThoiGianTuVanNavigation).WithMany(p => p.TuVanBangThoiGians)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TuVan_Ban__sMaTh__2B3F6F97");

            entity.HasOne(d => d.SMaTrangThaiNavigation).WithMany(p => p.TuVanBangThoiGians).HasConstraintName("FK__TuVan_Ban__sMaTrangT__276EDEB3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
