using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Models;

[Table("TuVan")]
[Index("SMaBacSi", Name = "IX_TuVan_sMaBacSi")]
public partial class TuVan
{
    [Key]
    [Column("sMaLich")]
    [StringLength(50)]
    public string SMaLich { get; set; } = null!;

    [Column("dNgayTuVan")]
    public DateOnly? DNgayTuVan { get; set; }

    [Column("sMaBacSi")]
    [StringLength(50)]
    public string? SMaBacSi { get; set; }

    [ForeignKey("SMaBacSi")]
    [InverseProperty("TuVans")]
    public virtual BacSi? SMaBacSiNavigation { get; set; }

    [InverseProperty("SMaLichNavigation")]
    public virtual ICollection<TuVanBangThoiGian> TuVanBangThoiGians { get; set; } = new List<TuVanBangThoiGian>();
}
