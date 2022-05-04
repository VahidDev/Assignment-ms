using Assignment.DataSeeding.DefaultEntityGenerators;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;

namespace Assignment.DataSeeding
{
    public static class DataSeeder
    {
        public static async void Seed(this WebApplication app)
        {
            AppDbContext context=app.Services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
            if(!context.IdDocumentTypes.Any())
                await IdDocumentTypesGenerator.GenerateIdDocumentTypesAsync(context);
            if(!context.PassportTypes.Any())
                await PassportTypesGenerator.GeneratePassportTypesAsync(context);
            if(!context.DrivingLicenseTypes.Any())
                await DrivingLicenseTypesGenerator.GenerateDrivingLicenseTypesAsync(context);
            if(!context.InternationalAccommodationPreferences.Any())
                await InternationalAccommodationPreferencesGenerator
                    .GenerateInternationalAccommodationPreferencesAsync(context);
        }
    }
}
