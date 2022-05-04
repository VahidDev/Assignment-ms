using DomainModels.Models.Entities;
using Repository.DAL;

namespace Assignment.DataSeeding.DefaultEntityGenerators
{
    public static class PassportTypesGenerator
    {
        public static async Task GeneratePassportTypesAsync(AppDbContext context)
        {
            await context.PassportTypes.AddRangeAsync(new List<PassportType> { 
            new PassportType { Name ="Official (Normal) Passport"},
            new PassportType { Name ="Royal/Private Passport"},
            new PassportType { Name ="Diplomatic Passport"},
            new PassportType { Name ="Mission Passport"},
            new PassportType { Name ="Travel Document"},
            new PassportType { Name ="Service Passport"},
            });
            await context.SaveChangesAsync();
        }
    }
}
