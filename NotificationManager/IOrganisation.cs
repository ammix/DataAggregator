using DatabaseAccess;

namespace NotificationManger;

public interface IOrganisation
{
    IQueryable<NotificationsBroker> GetSilentCustomers(int year, int month, int threshold);
}
