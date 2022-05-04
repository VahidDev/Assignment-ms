using DomainModels.Models.Entities;
using Repository.DAL;

namespace Assignment.DataSeeding.DefaultEntityGenerators
{
    public static class DrivingLicenseTypesGenerator
    {
        public static async Task GenerateDrivingLicenseTypesAsync(AppDbContext context)
        {
            await context.DrivingLicenseTypes.AddRangeAsync(new List<DrivingLicenseType> { 
            new DrivingLicenseType { Name ="Motorcycle"},
            new DrivingLicenseType { Name ="Car"},
            new DrivingLicenseType { Name ="Minibus (up to 8 people)"},
            new DrivingLicenseType { Name ="Bus (over 8 people)"},
            });
            await context.SaveChangesAsync();
        }
    }
}
