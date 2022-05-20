using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("functional_area_types")]
    public class FunctionalAreaType:Entity
    {
        [Display(Name= "Role Offer - Functional Area Type")]
        [Column("name")]
        public string Name { get; set; }
        public ICollection<FunctionalArea> FunctionalAreas { get; set; }
        public ICollection<RoleOffer> RoleOffers { get; set; }
        public ICollection<FunctionalAreaTypeFunctionalArea> FunctionalAreaTypeFunctionalAreas 
        { get; set; }
    }
}
