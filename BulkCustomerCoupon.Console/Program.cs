using BulkCustomerCoupon.Console.APIClient;
using BulkCustomerCoupon.Console.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var testConnectionString = "Data Source=localhost;initial catalog=Coupon;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
var serviceProvider = new ServiceCollection()
            .AddDbContext<SqlServerDbContext>(opts => opts.UseSqlServer(testConnectionString))
            .BuildServiceProvider();
var sqlServerDbContext = serviceProvider.GetService<SqlServerDbContext>();

var ppvCustomers = await sqlServerDbContext.PPVCustomers.Where(x => x.IsCouponCreated == null).ToListAsync();

for(int i = 0; i < ppvCustomers.Count; i++)
{
    var ppVCustomer = ppvCustomers[i];
    CouponAPIClient couponAPIClient = new CouponAPIClient();
    var isCreated = await couponAPIClient.CreatePPVCoupon(ppVCustomer.CustomerId);
    ppVCustomer.IsCouponCreated = isCreated;
    await sqlServerDbContext.SaveChangesAsync();
}

