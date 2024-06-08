using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Models;

[Table("TrangThaiTuVan")]
public partial class TrangThaiTuVan
{
    [Key]
    [Column("sMaTrangThai")]
    [StringLength(50)]
    public string SMaTrangThai { get; set; } = null!;

    [Column("sTenTrangThai")]
    [StringLength(100)]
    public string? STenTrangThai { get; set; }

    [Column("sMoTaTrangThai")]
    [StringLength(255)]
    public string? SMoTaTrangThai { get; set; }

    [InverseProperty("SMaTrangThaiNavigation")]
    public virtual ICollection<TuVanBangThoiGian> TuVanBangThoiGians { get; set; } = new List<TuVanBangThoiGian>();
}
