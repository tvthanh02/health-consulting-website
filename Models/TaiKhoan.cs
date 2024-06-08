using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Models;

[Table("TaiKhoan")]
[Index("SMaQuyen", Name = "IX_TaiKhoan_sMaQuyen")]
public partial class TaiKhoan
{
    [Key]
    [Column("sMaTaiKhoan")]
    [StringLength(50)]
    [BindNever]
    public string SMaTaiKhoan { get; set; } = null!;

    [Column("sTenDangNhap")]
    [StringLength(50)]
    [Required]
    [BindProperty]
    public string? STenDangNhap { get; set; }

    [Column("sMatKhau")]
    [StringLength(100)]
    [Required]
    [BindProperty]
    public string? SMatKhau { get; set; }

    [Column("sMaQuyen")]
    [StringLength(50)]
    [BindNever]
    public string? SMaQuyen { get; set; }

    [InverseProperty("SMaTaiKhoanNavigation")]
    public virtual ICollection<BacSi> BacSis { get; set; } = new List<BacSi>();

    [InverseProperty("SMaTaiKhoanNavigation")]
    public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();

    [ForeignKey("SMaQuyen")]
    [InverseProperty("TaiKhoans")]
    public virtual Quyen? SMaQuyenNavigation { get; set; }
}
