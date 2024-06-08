using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Models;

[Table("BacSi")]
[Index("SMaChuyenKhoa", Name = "IX_BacSi_sMaChuyenKhoa")]
[Index("SMaDonViCongTac", Name = "IX_BacSi_sMaDonViCongTac")]
[Index("SMaTaiKhoan", Name = "IX_BacSi_sMaTaiKhoan")]
public partial class BacSi
{
    [Key]
    [Column("sMaBacSi")]
    [StringLength(50)]
    public string SMaBacSi { get; set; } = null!;

    [Column("sTenBacSi")]
    [StringLength(100)]
    public string? STenBacSi { get; set; }

    [Column("sAvatar")]
    [StringLength(255)]
    public string? SAvatar { get; set; }

    [Column("intNamSinh")]
    public int? IntNamSinh { get; set; }

    [Column("smMoTa")]
    [StringLength(255)]
    public string? SmMoTa { get; set; }

    [Column("sMaChuyenKhoa")]
    [StringLength(50)]
    public string? SMaChuyenKhoa { get; set; }

    [Column("sMaTaiKhoan")]
    [StringLength(50)]
    public string? SMaTaiKhoan { get; set; }

    [Column("sMaDonViCongTac")]
    [StringLength(50)]
    public string? SMaDonViCongTac { get; set; }

    [Column("fPriceConsult")]
    public double? FPriceConsult { get; set; }

    [ForeignKey("SMaChuyenKhoa")]
    [InverseProperty("BacSis")]
    public virtual ChuyenKhoa? SMaChuyenKhoaNavigation { get; set; }

    [ForeignKey("SMaDonViCongTac")]
    [InverseProperty("BacSis")]
    public virtual DonViCongTac? SMaDonViCongTacNavigation { get; set; }

    [ForeignKey("SMaTaiKhoan")]
    [InverseProperty("BacSis")]
    public virtual TaiKhoan? SMaTaiKhoanNavigation { get; set; }

    [InverseProperty("SMaBacSiNavigation")]
    public virtual ICollection<TuVan> TuVans { get; set; } = new List<TuVan>();
}
