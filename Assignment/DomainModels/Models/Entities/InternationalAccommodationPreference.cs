using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("international_accommodation_preferences")]
    public class InternationalAccommodationPreference: Entity
    {
        [Column("name")]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
