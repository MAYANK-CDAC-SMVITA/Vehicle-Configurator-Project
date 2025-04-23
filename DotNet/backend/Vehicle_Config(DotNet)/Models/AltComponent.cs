using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Vehicle_Config_DotNet_.Models;

[Table("alt_components")]
[Index("AltCompId", Name = "FKgvcwehmbldv5wtks58m5pxipg")]
[Index("CompId", Name = "FKh6wwkiru12oixcdy55to8y3xp")]
[Index("ModelId", Name = "FKiaxybepfrjxkrov9xnwl6fbtv")]
public partial class AltComponent
{
    [Key]
    [Column("alt_id")]
    public long AltId { get; set; }

    [Column("price_variation")]
    public double PriceVariation { get; set; }

    [Column("alt_comp_id")]
    public long AltCompId { get; set; }

    [Column("comp_id")]
    public long CompId { get; set; }

    [Column("model_id")]
    public long ModelId { get; set; }

    [ForeignKey("AltCompId")]
    [InverseProperty("AltComponentAltComps")]
    [JsonIgnore]
    public virtual Component AltComp { get; set; } = null!;

    [ForeignKey("CompId")]
    [InverseProperty("AltComponentComps")]
    [JsonIgnore]
    public virtual Component Comp { get; set; } = null!;


    [ForeignKey("ModelId")]
    [InverseProperty("AltComponents")]
    [JsonIgnore]
    public virtual Model Model { get; set; } = null!;
}
