using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Models;

[Table("Quyen")]
public partial class Quyen
{
    [Key]
    [Column("sMaQuyen")]
    [StringLength(50)]
    public string SMaQuyen { get; set; } = null!;

    [Column("sTenQuyen")]
    [StringLength(255)]
    public string STenQuyen { get; set; } = null!;

    [InverseProperty("SMaQuyenNavigation")]
    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
