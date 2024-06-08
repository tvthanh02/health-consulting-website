using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Models;

[Table("ChuyenKhoa")]
public partial class ChuyenKhoa
{
    [Key]
    [Column("sMaChuyenKhoa")]
    [StringLength(50)]
    public string SMaChuyenKhoa { get; set; } = null!;

    [Column("sTenChuyenKhoa")]
    [StringLength(100)]
    [Required]
    public string? STenChuyenKhoa { get; set; }

    [Column("sMota")]
    [Required]
    public string? SMota { get; set; }

    [Column("sthumbnail")]
    [StringLength(50)]
    public string? Sthumbnail { get; set; }


    [InverseProperty("SMaChuyenKhoaNavigation")]
    public virtual ICollection<BacSi> BacSis { get; set; } = new List<BacSi>();
}
