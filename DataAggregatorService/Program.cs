using NotificationManger;

namespace DataAggregatorService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Add DI

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSpaStaticFiles(c => { c.RootPath = "wwwroot"; });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.MapGet("/addNotifications", (HttpContext httpContext) =>
            {
                var notificationManager = new NotificationManager();
                notificationManager.AddNotifications(year: 2024, month: 4, threshold: 3);
            })
            .WithName("AddNotifications")
            .WithOpenApi();

            app.Run();
        }
    }
}
