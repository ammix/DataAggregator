using NotificationManger;

namespace DataAggregatorService
{
    public class Program
    {
        public static void Main(string[] args)
        {
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

            var notificationManager = new NotificationManager();

            app.MapGet("/addNotifications", (HttpContext httpContext) =>
            {
                notificationManager.AddNotifications(year: 2024, month: 4, threshold: 3);
            })
            .WithName("AddNotifications")
            .WithOpenApi();

            app.Run();
        }
    }
}
