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
        

    }
}
