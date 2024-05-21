using DatabaseAccess;
using Microsoft.EntityFrameworkCore;

namespace NotificationManger
{
    public class NotificationManager
    {
        readonly AggregatorDbContext dbContext;
        readonly List<IOrganisation> organisations;

        public NotificationManager()
        {
            dbContext = new AggregatorDbContext();
            organisations =
                    [
                        new Organisation2(dbContext),
                        new Organisation101(dbContext),
                        new Organisation145(dbContext)
                    ];
        }

        public void AddNotifications(int year, int month, int threshold)
        {
            foreach (var organisation in organisations)
            {
                foreach (var customerData in organisation.GetSilentCustomers(year, month, threshold).AsNoTracking())
                    dbContext.NotificationsBrokers.Add(customerData);

                dbContext.SaveChanges();
            }
        }
    }
}
