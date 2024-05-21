using DatabaseAccess;

namespace NotificationManger;

public class Organisation145 : Organisation
{
    public Organisation145(AggregatorDbContext dbContext) : base(dbContext)
    { }

    protected override int OrganisationId => 145;

    public override IQueryable<NotificationsBroker> GetSilentCustomers(int year, int month, int threshold)
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
                FirstName = NameSplitter.SplitName(g.Key.Name).FirstName,
                LastName = NameSplitter.SplitName(g.Key.Name).LastName,
                FinHash = ClientCodeGenerator.GenerateCode(
                    NameSplitter.SplitName(g.Key.Name).FirstName,
                    NameSplitter.SplitName(g.Key.Name).LastName,
                    organisationName)
            })
            .Distinct();
    }
}
