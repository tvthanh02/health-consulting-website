using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Models;

[Table("Session")]
public partial class Session
{
    [Key]
    [Column("sSessionId")]
    [StringLength(100)]
    public string SSessionId { get; set; } = null!;

    [Column("sSessionData")]
    public string SSessionData { get; set; } = null!;

    [Column("dExpireTime", TypeName = "datetime")]
    public DateTime DExpireTime { get; set; }
}
