using BulkCustomerCoupon.Console.APIClient;
using BulkCustomerCoupon.Console.Data;
using BulkCustomerCoupon.Console.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var testConnectionString = "Data Source=localhost;initial catalog=Coupon;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
var oracleConnectionString = "User Id=hikmat;Password=hikmat;Data Source=192.168.20.123:1521/orcl;Connect Timeout=60";
var serviceProvider = new ServiceCollection()
            .AddDbContext<SqlServerDbContext>(opts => opts.UseSqlServer(testConnectionString))
            .AddDbContext<OracleDBContext>(opts => opts.UseOracle(oracleConnectionString))
            .BuildServiceProvider();
var sqlServerDbContext = serviceProvider.GetService<SqlServerDbContext>();
var oracleDbContext = serviceProvider.GetService<OracleDBContext>();

//var isConnected = oracleDbContext.Database.CanConnect();
IList<PPVCustomer> ppvCustomers = new List<PPVCustomer>();

//var ppvCustomersGroup = await sqlServerDbContext.PPVCustomers.GroupBy(x => x.CustomerId).ToListAsync();

var ppvCustomersGroup = await oracleDbContext.PPVCustomers
    .FromSqlRaw("select * from tbl_rch_coupun_all where lower(INVOICE_LINE_TEXT) not like '%bonus%' and lower(INVOICE_LINE_TEXT) not like '%reverse%' ;")
    .OrderBy(x => x.CreatedDateTime)
    .GroupBy(x => x.CustomerId).ToListAsync();

foreach (var group in ppvCustomersGroup)
{
    var customer = group.OrderByDescending(x => x.IsCouponCreated).FirstOrDefault();
    if (customer.IsCouponCreated is null || !customer.IsCouponCreated.Value)
    {
        ppvCustomers.Add(customer);
    }
}

for (int i = 0; i < ppvCustomers.Count; i++)
{
    var ppVCustomer = ppvCustomers[i];
    var couponType = await sqlServerDbContext.CouponTypeFromDenos.Where(x => x.Deno == ppVCustomer.Deno).Select(x => x.CouponType).FirstOrDefaultAsync();
    CouponAPIClient couponAPIClient = new CouponAPIClient();
    var isCreated = await couponAPIClient.CreatePPVCoupon(ppVCustomer.CustomerId, couponType);
    ppVCustomer.IsCouponCreated = isCreated;
    await sqlServerDbContext.SaveChangesAsync();
}

