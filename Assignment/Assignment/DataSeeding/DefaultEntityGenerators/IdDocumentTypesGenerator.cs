using DomainModels.Models.Entities;
using Repository.DAL;

namespace Assignment.DataSeeding.DefaultEntityGenerators
{
    public static class IdDocumentTypesGenerator
    {
        public static async Task GenerateIdDocumentTypesAsync(AppDbContext context)
        {
            await context.IdDocumentTypes.AddRangeAsync(new List<IdDocumentType> { 
            new IdDocumentType{Name="QID"},
            new IdDocumentType{Name="Passport"}
            });
            await context.SaveChangesAsync();
        }
    }
}
