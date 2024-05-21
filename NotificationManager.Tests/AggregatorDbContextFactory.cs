using DatabaseAccess;
using Microsoft.EntityFrameworkCore;

namespace NotificationManager.Tests
{
    public class AggregatorDbContextFactory
    {
        public static AggregatorDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AggregatorDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new AggregatorDbContext(options);
            context.Database.EnsureCreated();

            // TODO: add DB test tables

            context.SaveChanges();
            return context;
        }

        public static void Destroy(AggregatorDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
