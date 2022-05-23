using DomainModels.Dtos.Abstraction;
using DomainModels.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("requirements")]
    public class Requirement : Entity, IValueFromArrayConvertible
    {
        [Column("requirement_name")]
        [Display(Name = "Requirement")]
        public string RequirementName { get; set; }
        [Column("operator")]
        [Display(Name = "Operator")]
        public string Operator { get; set; }
        [Column("value")]
        [Display(Name = "Value")]
        public string Value { get; set; }
        [NotMapped]
        [Display(Name = "Role Offer ID")]
        public int RoleOfferId { get; set; }
        [Column("functional_requirement_id")]
        [Display(Name ="Functional Requirement ID")]
        public int FunctionalRequirementId { get; set; }
        public FunctionalRequirement FunctionalRequirement { get; set; }
        [NotMapped]
        [Display(Name = "Level Of Confidence")]
        public int LevelOfConfidence { get; set; }
        [NotMapped]
        [Display(Name = "WaitList Count")]
        public int WaitlistCount { get; set; }
    }
}
