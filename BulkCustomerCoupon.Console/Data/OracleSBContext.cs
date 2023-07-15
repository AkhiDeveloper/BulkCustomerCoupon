using BulkCustomerCoupon.Console.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkCustomerCoupon.Console.Data
{
    public class OracleDBContext
        : DbContext
    {
        public OracleDBContext(DbContextOptions<OracleDBContext> options)
            : base(options)
        {
            
        }

        public DbSet<CouponTypeFromDeno> CouponTypeFromDenos { get; set; }
        public DbSet<PPVCustomer> PPVCustomers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PPVCustomer>().ToTable("tbl_rch_coupun_all");
            modelBuilder.Entity<PPVCustomer>().Property(p => p.CustomerId)
                .HasColumnName("CUSTOMER_ID");
            modelBuilder.Entity<PPVCustomer>().Property(p => p.Deno)
                .HasColumnName("BASE_AMOUNT");
            modelBuilder.Entity<PPVCustomer>().Property(p => p.CreatedDateTime)
                .HasColumnName("CREATE_DATETIME")
                .HasColumnType("DATE");
            modelBuilder.Entity<PPVCustomer>().Property(p => p.IsCouponCreated)
                .HasColumnName("IS_COUPON_CREATED");
            base.OnModelCreating(modelBuilder);
        }

    }
}
