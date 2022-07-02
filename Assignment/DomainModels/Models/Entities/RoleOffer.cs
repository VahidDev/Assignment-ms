using DomainModels.Constants;
using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("role_offers")]
    public class RoleOffer : Entity
    {
        [ForeignKey("functional_area_type_id")]
        [Display(Name = DisplayAttributeConstants.CustomObject + " FunctionalAreaType")]
        public FunctionalAreaType FunctionalAreaType { get; set; }
        [ForeignKey("functional_area_id")]
        [Display(Name = DisplayAttributeConstants.CustomObject + " FunctionalArea")]
        public FunctionalArea FunctionalArea { get; set; }
        [ForeignKey("job_title_id")]
        [Display(Name = DisplayAttributeConstants.CustomObject + " JobTitle")]
        public JobTitle JobTitle { get; set; }
        [ForeignKey("location_id")]
        [Display(Name = DisplayAttributeConstants.CustomObject + " Location")]
        public Location Location { get; set; }
        [Column("role_offer_id")]
        [Display(Name = "Role Offer - ID")]
        public int RoleOfferId { get; set; }
        [Column("total_demand")]
        [Display(Name = "Role Offer - Total Demand")]
        public int TotalDemand { get; set; }
        [Column("assignee_demand")]
        public int AssigneeDemand { get; set; }
        [Column("waitlist_demand")]
        public int WaitlistDemand { get; set; } = 0;
        [Column("level_of_confidence")]
        public int? LevelOfConfidence { get; set; } = 100;
        public ICollection<Volunteer> Volunteers { get; set; }
        [NotMapped]
        public FunctionalRequirement FunctionalRequirement { get; set; }
        public ICollection<History> Histories { get; set; }
    }
}
