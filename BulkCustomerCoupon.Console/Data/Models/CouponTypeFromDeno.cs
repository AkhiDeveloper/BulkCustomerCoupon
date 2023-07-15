using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkCustomerCoupon.Console.Data.Models
{
    [Table("CouponTypeFromDenos")]
    public class CouponTypeFromDeno
    {
        [Key]
        public int Id { get; set; }
        public int Deno { get; set; }
        public int CouponType { get; set; }
    }
}
