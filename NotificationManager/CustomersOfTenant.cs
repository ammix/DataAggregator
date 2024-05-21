using DatabaseAccess;
using Microsoft.EntityFrameworkCore;

namespace NotificationManger;

public abstract class CustomersOfTenant : ICustomersOfTenant
{
    protected readonly AggregatorDbContext dbContext;

    protected CustomersOfTenant(AggregatorDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    protected string? GetOrgranisationName()
    {
        return dbContext.Tenants
                                .Where(t => t.Id == OrganisationId)
                                .Select(t => t.OrganisationName)
                                .FirstOrDefault();
    }

    protected abstract int OrganisationId { get; }

    protected abstract IQueryable<NotificationsBroker> GetQuery(int year, int month, int threshold);

    protected virtual NotificationsBroker GetNotificationBroker(NotificationsBroker item, string? orgName)
    {
        return new NotificationsBroker
        {
            Email = item.Email,
            FirstName = item.FirstName,
            LastName = item.LastName,
            FinHash = ClientCodeGenerator.GenerateCode(item.FirstName, item.LastName, orgName)
        };
    }

    public void AddNotifications(int year, int month, int threshold)
    {
        var orgName = GetOrgranisationName();
        var query = GetQuery(year, month, threshold).AsNoTracking();

        foreach (var item in query)
        {
            var newNotification = GetNotificationBroker(item, orgName);

            dbContext.NotificationsBrokers.Add(newNotification);
        }
    }
}

