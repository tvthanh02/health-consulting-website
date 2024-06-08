using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Models;

[Table("BangThoiGianTuVan")]
public partial class BangThoiGianTuVan
{
    [Key]
    [Column("sMaThoiGianTuVan")]
    [StringLength(50)]
    public string SMaThoiGianTuVan { get; set; } = null!;

    [Column("tThoiGianBatDau")]
    public TimeOnly? TThoiGianBatDau { get; set; }

    [Column("tThoiGianKetThuc")]
    public TimeOnly? TThoiGianKetThuc { get; set; }

    [InverseProperty("SMaThoiGianTuVanNavigation")]
    public virtual ICollection<TuVanBangThoiGian> TuVanBangThoiGians { get; set; } = new List<TuVanBangThoiGian>();
}
