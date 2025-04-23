using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Vehicle_Config_DotNet_.Models;

[Table("manufacturers")]
[Index("SegId", Name = "FKvt86n4h6jurg9ofnq26txaw7")]
public partial class Manufacturer
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("manu_name")]
    [StringLength(255)]
    public string ManuName { get; set; } = null!;

    [Column("seg_id")]
    public int SegId { get; set; }

    [InverseProperty("Manu")]
    public virtual ICollection<Model> Models { get; set; } = new List<Model>();

    [ForeignKey("SegId")]
    [InverseProperty("Manufacturers")]

    [JsonIgnore]
    public virtual Segment Seg { get; set; } = null!;
}
