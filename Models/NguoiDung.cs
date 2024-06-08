using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Models;

[Table("NguoiDung")]
[Index("SMaTaiKhoan", Name = "IX_NguoiDung_sMaTaiKhoan")]
public partial class NguoiDung
{
    [Key]
    [Column("sMaNguoiDung")]
    [StringLength(50)]
    [BindNever]
    public string SMaNguoiDung { get; set; } = null!;

    [Column("sHoTen")]
    [StringLength(100)]
    [Required]
    [BindProperty]
    public string? SHoTen { get; set; }

    [Column("dNgaySinh")]
    [Required]
    [BindProperty]
    public DateOnly? DNgaySinh { get; set; }

    [Column("bGioiTinh")]
    [StringLength(10)]
    [Required]
    [BindProperty]
    public string? BGioiTinh { get; set; }

    [Column("sSDT")]
    [StringLength(15)]
    [Required]
    [BindProperty]
    public string? SSdt { get; set; }

    [Column("smDiaChi")]
    [StringLength(255)]
    public string? SmDiaChi { get; set; }

    [Column("sMaTaiKhoan")]
    [StringLength(50)]
    [BindNever]
    public string? SMaTaiKhoan { get; set; }

    [ForeignKey("SMaTaiKhoan")]
    [InverseProperty("NguoiDungs")]
    public virtual TaiKhoan? SMaTaiKhoanNavigation { get; set; }

    [InverseProperty("SMaNguoiDungNavigation")]
    public virtual ICollection<TuVanBangThoiGian> TuVanBangThoiGians { get; set; } = new List<TuVanBangThoiGian>();
}
