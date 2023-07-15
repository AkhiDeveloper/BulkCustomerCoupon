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
            //CouponTypeFromDeno Table mapping config
            modelBuilder.Entity<CouponTypeFromDeno>().ToTable("COUPONTYPEFROMDENOS");
            modelBuilder.Entity<CouponTypeFromDeno>().HasKey(x => x.Id);
            modelBuilder.Entity<CouponTypeFromDeno>().Property(x =>x.Id).HasColumnName("ID");
            modelBuilder.Entity<CouponTypeFromDeno>().Property(x => x.Deno).HasColumnName("DENO");
            modelBuilder.Entity<CouponTypeFromDeno>().Property(x => x.CouponType).HasColumnName("COUPONTYPE");
            modelBuilder.Entity<PPVCustomer>().ToTable("TBL_RCH_COUPUN_ALL");

            //PPVCustomer Table mapping config
            modelBuilder.Entity<PPVCustomer>().HasKey(x => x.Id);
            modelBuilder.Entity<PPVCustomer>().Property(x => x.Id).HasColumnName("ID");
            modelBuilder.Entity<PPVCustomer>().Property(p => p.CustomerId)
                .HasColumnName("CUSTOMER_ID");
            modelBuilder.Entity<PPVCustomer>().Property(p => p.Deno)
                .HasColumnName("BASE_AMOUNT");
            modelBuilder.Entity<PPVCustomer>().Property(p => p.CreatedDateTime)
                .HasColumnName("CREATE_DATETIME")
                .HasColumnType("DATE");
            modelBuilder.Entity<PPVCustomer>().Property(p => p.IsCouponCreated)
                .HasColumnName("IS_COUPON_CREATED");
            modelBuilder.Entity<PPVCustomer>().Property(p => p.InvoicLineText)
                .HasColumnName("INVOICE_LINE_TEXT");
            base.OnModelCreating(modelBuilder);
        }

    }
}
