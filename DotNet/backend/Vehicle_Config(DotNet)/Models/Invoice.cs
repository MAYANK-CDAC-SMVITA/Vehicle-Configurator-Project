using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicle_Config_DotNet_.Models
{
    [Table("invoice")]
    public partial class Invoice
    {
        [Key]
        [Column("inv_id")]
        public int InvId { get; set; }


        [Column("components")]
        public string? Components { get; set; }

        [Column("date")]
        [StringLength(255)]
        public string? Date { get; set; }

        [Column("gst_amount")]
        public double? GstAmount { get; set; }

        [Column("invoice_id")]
        [StringLength(255)]
        public string? InvoiceId { get; set; }

        [Column("model_name")]
        [StringLength(255)]
        public string? ModelName { get; set; }

        [Column("total_price")]
        public double? TotalPrice { get; set; }

        [Column("total_withgst")]
        public double? TotalWithgst { get; set; }

        [Column("email")]
        [StringLength(255)]
        public string? Email { get; set; }
    }
    }
