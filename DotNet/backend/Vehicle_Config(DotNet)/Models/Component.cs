using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Vehicle_Config_DotNet_.Models;

[Table("components")]
public partial class Component
{
    [Key]
    [Column("comp_id")]
    public long CompId { get; set; }

    [Column("comp_name")]
    [StringLength(255)]
    public string CompName { get; set; } = null!;

    [InverseProperty("AltComp")]
    [JsonIgnore]
    public  ICollection<AltComponent> AltComponentAltComps { get; set; } = new List<AltComponent>();

    [JsonIgnore]
    [InverseProperty("Comp")]
    //[JsonIgnore]
    public  ICollection<AltComponent> AltComponentComps { get; set; } = new List<AltComponent>();

    [JsonIgnore]
    [InverseProperty("Comp")]
    //[JsonIgnore]
    public  ICollection<VehicleDetail> VehicleDetails { get; set; } = new List<VehicleDetail>();
}
