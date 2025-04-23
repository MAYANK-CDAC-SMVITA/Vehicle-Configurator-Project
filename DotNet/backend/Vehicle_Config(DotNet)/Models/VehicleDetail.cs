using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Vehicle_Config_DotNet_.Models;

[Table("vehicle_details")]
[Index("ModelId", Name = "FK4xnsh93sp191ava4jva94peds")]
[Index("CompId", Name = "FKq35ocm7yckn0ak8jrsk06l0c3")]
public partial class VehicleDetail
{
    [Key]
    [Column("confi_id")]
    public long ConfiId { get; set; }

    [Column("comp_type")]
    [StringLength(1)]
    public string CompType { get; set; } = null!;

    [Column("is_configurable")]
    [StringLength(1)]
    public string IsConfigurable { get; set; } = null!;

    [Column("comp_id")]
    public long CompId { get; set; }

    [Column("model_id")]
    public long ModelId { get; set; }

    [ForeignKey("CompId")]
    [InverseProperty("VehicleDetails")]
    [JsonIgnore]
    public virtual Component Comp { get; set; } = null!;

    [ForeignKey("ModelId")]
    [InverseProperty("VehicleDetails")]
    [JsonIgnore]
    public virtual Model Model { get; set; } = null!;
}
