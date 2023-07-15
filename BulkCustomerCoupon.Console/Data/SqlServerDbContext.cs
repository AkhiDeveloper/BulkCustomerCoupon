using BulkCustomerCoupon.Console.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkCustomerCoupon.Console.Data
{
    internal class SqlServerDbContext
        : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<tbl_rch_coupun_all> PPVCustomers { get; set; }
        public DbSet<CouponTypeFromDeno> CouponTypeFromDenos { get; set; }
    }
}
