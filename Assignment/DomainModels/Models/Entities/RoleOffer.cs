using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("role_offers")]
    public class RoleOffer : Entity
    {
        [Display(Name = "Custom Object FunctionalAreaType")]
        [ForeignKey("functional_area_type_id")]
        public FunctionalAreaType FunctionalAreaType { get; set; }
        [Display(Name = "Custom Object FunctionalArea")]
        [ForeignKey("functional_area_id")]
        public FunctionalArea FunctionalArea { get; set; }
        [Display(Name = "Custom Object JobTitle")]
        [ForeignKey("job_title_id")]
        public JobTitle JobTitle { get; set; }
        [Display(Name = "Custom Object Location")]
        [ForeignKey("location_id")]
        public Location Location { get; set; }
        [Display(Name = "Role Offer - ID")]
        [Column("role_offer_id")]
        public int RoleOfferId { get; set; }
        [Display(Name = "Role Offer - Total Demand")]
        [Column("total_demand")]
        public int TotalDemand { get; set; }
        public ICollection<Volunteer> Volunteers { get; set; }
    }
}
