using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkCustomerCoupon.Console.Data.Models
{
    public class tbl_rch_coupun_all
    {
        [Key]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int Deno { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? InvoicLineText { get; set; }
        public bool? IsCouponCreated { get; set; }
    }
}
