using DomainModels.Constants;
using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table(TableNameConstants.FunctionalAreaTypeTableName)]
    public class FunctionalAreaType:Entity
    {
        [Column("name")]
        [Display(Name= "Role Offer - Functional Area Type")]
        public string Name { get; set; }
        public ICollection<FunctionalArea> FunctionalAreas { get; set; }
        public ICollection<RoleOffer> RoleOffers { get; set; }
        public ICollection<FunctionalAreaTypeFunctionalArea> 
            FunctionalAreaTypeFunctionalAreas { get; set; }
    }
}
