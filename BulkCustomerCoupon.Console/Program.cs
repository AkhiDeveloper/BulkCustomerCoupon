using BulkCustomerCoupon.Console.APIClient;
using BulkCustomerCoupon.Console.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Oracle.ManagedDataAccess.Client;

var testConnectionString = "Data Source=localhost;initial catalog=Coupon;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
var oracleConnectionString = "Provider=OraOLEDB.Oracle;Data Source=192.168.20.123:1521;User Id=hikmat;Password=hikmat;";
var serviceProvider = new ServiceCollection()
            .AddDbContext<SqlServerDbContext>(opts => opts.UseSqlServer(testConnectionString))
            .AddDbContext<OracleDBContext>(opts => opts.UseOracle(oracleConnectionString))
            .BuildServiceProvider();
var sqlServerDbContext = serviceProvider.GetService<SqlServerDbContext>();
var oracleDbContext = serviceProvider.GetService<OracleDBContext>();

var isConnected = oracleDbContext.Database.CanConnect();
var ppvCustomers = await sqlServerDbContext.PPVCustomers.Where(x => x.IsCouponCreated == null).ToListAsync();

for(int i = 0; i < ppvCustomers.Count; i++)
{
    var ppVCustomer = ppvCustomers[i];
    CouponAPIClient couponAPIClient = new CouponAPIClient();
    var isCreated = await couponAPIClient.CreatePPVCoupon(ppVCustomer.CustomerId);
    ppVCustomer.IsCouponCreated = isCreated;
    await sqlServerDbContext.SaveChangesAsync();
}

