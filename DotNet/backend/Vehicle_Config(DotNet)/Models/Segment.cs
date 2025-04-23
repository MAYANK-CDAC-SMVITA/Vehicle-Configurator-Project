using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Vehicle_Config_DotNet_.Models;

[Table("segments")]
public partial class Segment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("seg_name")]
    [StringLength(255)]
    public string SegName { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("Seg")]
    public virtual ICollection<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();

    [JsonIgnore]
    [InverseProperty("Seg")]
   public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
