using DatabaseAccess;
using Microsoft.EntityFrameworkCore;

namespace NotificationManger;

public abstract class Organisation : IOrganisation
{
    protected readonly string? organisationName;
    protected readonly AggregatorDbContext dbContext;

    public abstract IQueryable<NotificationsBroker> GetSilentCustomers(int year, int month, int threshold);

    protected abstract int OrganisationId { get; }

    protected Organisation(AggregatorDbContext dbContext)
    {
        this.dbContext = dbContext;
        organisationName = GetOrgranisationName();
    }

    string? GetOrgranisationName()
    {
        return dbContext.Tenants
                                .Where(t => t.Id == OrganisationId)
                                .Select(t => t.OrganisationName)
                                .FirstOrDefault();
    }
}
