using DatabaseAccess;

namespace NotificationManger
{
    public class NotificationManager
    {
        readonly AggregatorDbContext dbContext;
        readonly List<ICustomersOfTenant> customersOfTenants;

        public NotificationManager()
        {
            dbContext = new AggregatorDbContext();
            customersOfTenants = new List<ICustomersOfTenant>
                    {
                        new CustomersOfTenant2(dbContext),
                        new CustomersOfTenant101(dbContext),
                        new CustomersOfTenant145(dbContext)
                    };
        }

        public void AddNotifications(int year, int month, int threshold)
        {
            foreach (var tenant in customersOfTenants)
                tenant.AddNotifications(year, month, threshold);

            dbContext.SaveChanges();
        }
    }
}
