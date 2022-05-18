using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("entities")]
    public class ExcelEntity:Entity
    {
        [Display(Name="Entity")]
        [Column("name")]
        public string Name { get; set; }
        [Display(Name = "Entity ID")]
        [Column("entity_id")]
        public int ExcelEId { get; set; }

        public ICollection<FunctionalArea> FunctionalAreas { get; set; }
        public ICollection<RoleOffer> RoleOffers { get; set; }
        public ICollection<ExcelEntityFunctionalAreas> ExcelEntityFunctionalAreas { get; set; }
    }
}
