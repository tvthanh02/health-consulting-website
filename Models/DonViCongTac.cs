using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Models;

[Table("DonViCongTac")]
public partial class DonViCongTac
{
    [Key]
    [Column("sMaDonVi")]
    [StringLength(50)]
    public string SMaDonVi { get; set; } = null!;

    [Column("sTenDonVi")]
    [StringLength(100)]
    public string? STenDonVi { get; set; }

    [InverseProperty("SMaDonViCongTacNavigation")]
    public virtual ICollection<BacSi> BacSis { get; set; } = new List<BacSi>();
}
