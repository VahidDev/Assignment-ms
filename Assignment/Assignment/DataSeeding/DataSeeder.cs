using Microsoft.EntityFrameworkCore;
using Repository.DAL;

namespace Assignment.DataSeeding
{
    public static class DataSeeder
    {
        public static void Seed(this WebApplication app)
        {
            using IServiceScope scope=app.Services.CreateScope();
            AppDbContext context=scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
    }
}
