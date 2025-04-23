using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vehicle_Config_DotNet_.Models;

[Table("company_info")]
public partial class CompanyInfo
{
    [Key]
    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("address_line1")]
    [StringLength(255)]
    public string? AddressLine1 { get; set; }

    [Column("address_line2")]
    [StringLength(200)]
    public string? AddressLine2 { get; set; }

    [Column("area_city")]
    [StringLength(255)]
    public string? AreaCity { get; set; }

    [Column("authorized_cell")]
    [StringLength(255)]
    public string? AuthorizedCell { get; set; }

    [Column("authorized_person_name")]
    [StringLength(255)]
    public string? AuthorizedPersonName { get; set; }

    [Column("authorized_tel")]
    [StringLength(255)]
    public string? AuthorizedTel { get; set; }

    [Column("company_name")]
    [StringLength(255)]
    public string? CompanyName { get; set; }

    [Column("designation")]
    [StringLength(255)]
    public string? Designation { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("fax")]
    [StringLength(255)]
    public string? Fax { get; set; }

    [Column("holding_type")]
    [StringLength(255)]
    public string? HoldingType { get; set; }

    [Column("pan_no")]
    [StringLength(255)]
    public string? PanNo { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string? Password { get; set; }

    [Column("pin_code")]
    [StringLength(255)]
    public string? PinCode { get; set; }

    [Column("st_no")]
    [StringLength(255)]
    public string? StNo { get; set; }

    [Column("state")]
    [StringLength(255)]
    public string? State { get; set; }

    [Column("tel")]
    [StringLength(255)]
    public string? Tel { get; set; }

    [Column("vat_reg_no")]
    [StringLength(255)]
    public string? VatRegNo { get; set; }
}
