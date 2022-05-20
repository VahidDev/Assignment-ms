using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("functional_requirements")]
    public class FunctionalRequirement : Entity
    {
        public ICollection<Requirement> Requirements { get; set; }
        [Column("excel_functional_requirement_id")]
        public int ExcelFunctionalRequirementId { get; set; }
        public RoleOffer RoleOffer { get; set; }
        [Column("role_offer_id")]
        public int RoleOfferId { get; set; }
    }
}
