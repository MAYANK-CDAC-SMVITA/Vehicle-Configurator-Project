using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Vehicle_Config_DotNet_.Models;

[Table("models")]
[Index("SegId", Name = "FKeoehxbf066gnexos8p4o9fn5e")]
[Index("ManuId", Name = "FKr7t3perk8n5abjcuk8j3kn9ko")]
public partial class Model
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("image_path")]
    [StringLength(255)]
    public string ImagePath { get; set; } = null!;

    [Column("min_qty")]
    public int MinQty { get; set; }

    [Column("mod_name")]
    [StringLength(255)]
    public string ModName { get; set; } = null!;

    [Column("price")]
    public int Price { get; set; }

    [Column("safety_rating")]
    public int? SafetyRating { get; set; }

    [Column("manu_id")]
    public long? ManuId { get; set; }

    [Column("seg_id")]
    public int? SegId { get; set; }

    [InverseProperty("Model")]
    [JsonIgnore]
    public virtual ICollection<AltComponent> AltComponents { get; set; } = new List<AltComponent>();

    [ForeignKey("ManuId")]
    [InverseProperty("Models")]
    public virtual Manufacturer? Manu { get; set; }

    [ForeignKey("SegId")]
    [InverseProperty("Models")]
    public virtual Segment? Seg { get; set; }

    [InverseProperty("Model")]
    [JsonIgnore]
    public virtual ICollection<VehicleDetail> VehicleDetails { get; set; } = new List<VehicleDetail>();
}
