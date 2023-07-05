using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkCustomerCoupon.Console.Data.Models
{
    internal class PPVCustomer
    {
        [Key]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public bool? IsCouponCreated { get; set; }
    }
}
