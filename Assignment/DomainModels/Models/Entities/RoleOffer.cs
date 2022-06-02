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
        [Display(Name = "Custom Object FunctionalAreaType")]
        public FunctionalAreaType FunctionalAreaType { get; set; }
        [ForeignKey("functional_area_id")]
        [Display(Name = "Custom Object FunctionalArea")]
        public FunctionalArea FunctionalArea { get; set; }
        [ForeignKey("job_title_id")]
        [Display(Name = "Custom Object JobTitle")]
        public JobTitle JobTitle { get; set; }
        [ForeignKey("location_id")]
        [Display(Name = "Custom Object Location")]
        public Location Location { get; set; }
        [Column("role_offer_id")]
        [Display(Name = "Role Offer - ID")]
        public int RoleOfferId { get; set; }
        [Column("total_demand")]
        [Display(Name = "Role Offer - Total Demand")]
        public int TotalDemand { get; set; }
        [Column("role_offer_fulfillment")]
        public int RoleOfferFulfillment { get; set; }
        [Column("waitlist_fulfillment")]
        public int WaitlistFulfillment { get; set; } 
        [Column("level_of_confidence")]
        public int? LevelOfConfidence { get; set; } = 100;
        [Column("waitlist_count")]
        public int? WaitlistCount { get; set; } = 0;
        public ICollection<Volunteer> Volunteers { get; set; }
        public FunctionalRequirement FunctionalRequirement { get; set; }
        public ICollection<History> Histories { get; set; }
    }
}
