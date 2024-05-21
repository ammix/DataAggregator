using DatabaseAccess;

namespace NotificationManger;

public class CustomersOfTenant145 : CustomersOfTenant
{
    public CustomersOfTenant145(AggregatorDbContext dbContext) : base(dbContext)
    { }

    protected override int OrganisationId => 145;

    protected override IQueryable<NotificationsBroker> GetQuery(int year, int month, int threshold)
    {
        return dbContext.Customer145s
            .GroupJoin(
                dbContext.Events145s,
                customer => customer.UserId,
                eventItem => eventItem.CustomerId,
                (customer, eventItems) => new { customer, eventItems }
            )
            .SelectMany(
                temp => temp.eventItems.DefaultIfEmpty(),
                (temp, eventItem) => new { temp.customer, eventItem }
            )
            .Where(x => x.eventItem == null || (x.eventItem.EventDate.Month == month && x.eventItem.EventDate.Year == year))
            .GroupBy(
                x => new { x.customer.UserId, x.customer.Email, x.customer.Name },
                x => x.eventItem
            )
            .Where(g => g.Count(eventItem => eventItem != null) < threshold)
            .Select(g => new NotificationsBroker
            {
                Email = g.Key.Email,
                FirstName = g.Key.Name,
                LastName = string.Empty,
            })
            .Distinct();
    }

    protected override NotificationsBroker GetNotificationBroker(NotificationsBroker item, string? orgName)
    {
        var firstName = NameSplitter.SplitName(item.FirstName).FirstName;
        var lastName = NameSplitter.SplitName(item.FirstName).LastName;

        return new NotificationsBroker
        {
            Email = item.Email,
            FirstName = firstName,
            LastName = lastName,
            FinHash = ClientCodeGenerator.GenerateCode(firstName, lastName, orgName)
        };
    }
}
