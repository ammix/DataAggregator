using DatabaseAccess;

namespace NotificationManger;

public class Organisation101 : Organisation
{
    public Organisation101(AggregatorDbContext dbContext) : base(dbContext)
    { }

    protected override int OrganisationId => 101;

    public override IQueryable<NotificationsBroker> GetSilentCustomers(int year, int month, int threshold)
    {
        return dbContext.Customer101s
            .GroupJoin(
                dbContext.Events101s,
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
                x => new { x.customer.Id, x.customer.Email, x.customer.FirstName, x.customer.LastName },
                x => x.eventItem
            )
            .Where(g => g.Count(eventItem => eventItem != null) < threshold)
            .Select(g => new NotificationsBroker
            {
                Email = g.Key.Email,
                FirstName = g.Key.FirstName,
                LastName = g.Key.LastName,
                FinHash = ClientCodeGenerator.GenerateCode(g.Key.FirstName, g.Key.LastName, organisationName)
            })
            .Distinct();
    }
}
