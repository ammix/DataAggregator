using DatabaseAccess;

namespace NotificationManger;

public class CustomersOfTenant2 : CustomersOfTenant
{
    public CustomersOfTenant2(AggregatorDbContext dbContext) : base(dbContext)
    { }

    protected override int OrganisationId => 2;

    protected override IQueryable<NotificationsBroker> GetQuery(int year, int month, int threshold)
    {
        return dbContext.Customer2s
            .GroupJoin(
                dbContext.Events2s,
                customer => customer.Id,
                eventItem => eventItem.CustomerId,
                (customer, eventItems) => new { customer, eventItems }
            )
            .SelectMany(
                temp => temp.eventItems.DefaultIfEmpty(),
                (temp, eventItem) => new { temp.customer, eventItem }
            )
            .Where(x => x.eventItem == null || (x.eventItem.EventDate.Month == month && x.eventItem.EventDate.Year == year))
            .GroupBy(
                x => new { x.customer.Id, x.customer.Email, x.customer.GivenName, x.customer.FamilyName },
                x => x.eventItem
            )
            .Where(g => g.Count(eventItem => eventItem != null) < threshold)
            .Select(g => new NotificationsBroker
            {
                Email = g.Key.Email,
                FirstName = g.Key.GivenName,
                LastName = g.Key.FamilyName
            })
            .Distinct();
    }
}
