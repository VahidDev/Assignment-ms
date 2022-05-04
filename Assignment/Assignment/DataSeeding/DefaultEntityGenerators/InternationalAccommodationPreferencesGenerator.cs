using DomainModels.Models.Entities;
using Repository.DAL;

namespace Assignment.DataSeeding.DefaultEntityGenerators
{
    public static class InternationalAccommodationPreferencesGenerator
    {
        public static async Task GenerateInternationalAccommodationPreferencesAsync
            (AppDbContext context)
        {
            await context.InternationalAccommodationPreferences.AddRangeAsync
                (new List<InternationalAccommodationPreference> 
                { 
                    new InternationalAccommodationPreference
                    {
                        Name ="Stay with friends or family"
                    },
                    new InternationalAccommodationPreference
                    {
                        Name ="Stay in a Hotel, Hostel or Apartment Rental"
                    }
                });
            await context.SaveChangesAsync();
        }
    }
}
