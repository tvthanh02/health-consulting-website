using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Models;

[PrimaryKey("SMaLich", "SMaThoiGianTuVan")]
[Table("TuVan_BangThoiGian")]
[Index("SMaThoiGianTuVan", Name = "IX_TuVan_BangThoiGian_sMaThoiGianTuVan")]
public partial class TuVanBangThoiGian
{
    [Key]
    [Column("sMaLich")]
    [StringLength(50)]
    public string SMaLich { get; set; } = null!;

    [Key]
    [Column("sMaThoiGianTuVan")]
    [StringLength(50)]
    public string SMaThoiGianTuVan { get; set; } = null!;

    [Column("sGhiChuBacSi")]
    [StringLength(255)]
    public string? SGhiChuBacSi { get; set; }

    [Column("sGhiChuNguoiDuocTuVan")]
    [StringLength(255)]
    public string? SGhiChuNguoiDuocTuVan { get; set; }

    [Column("iDanhGia")]
    public int? IDanhGia { get; set; }

    [Column("sMaNguoiDung")]
    [StringLength(50)]
    public string? SMaNguoiDung { get; set; }

    [Column("sMaTrangThai")]
    [StringLength(50)]
    public string? SMaTrangThai { get; set; }

    [ForeignKey("SMaLich")]
    [InverseProperty("TuVanBangThoiGians")]
    public virtual TuVan SMaLichNavigation { get; set; } = null!;

    [ForeignKey("SMaNguoiDung")]
    [InverseProperty("TuVanBangThoiGians")]
    public virtual NguoiDung? SMaNguoiDungNavigation { get; set; }

    [ForeignKey("SMaThoiGianTuVan")]
    [InverseProperty("TuVanBangThoiGians")]
    public virtual BangThoiGianTuVan SMaThoiGianTuVanNavigation { get; set; } = null!;

    [ForeignKey("SMaTrangThai")]
    [InverseProperty("TuVanBangThoiGians")]
    public virtual TrangThaiTuVan? SMaTrangThaiNavigation { get; set; }
}
