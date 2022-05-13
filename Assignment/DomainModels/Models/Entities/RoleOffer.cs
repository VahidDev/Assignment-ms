using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("role_offers")]
    public class RoleOffer : Entity
    {
        [ForeignKey("venue_id")]
        [Display(Name = "Venue")]
        public Venue Venue { get; set; }
        [ForeignKey("functional_area_id")]
        [Display(Name = "Project")]
        public FunctionalArea FunctionalArea { get; set; }
        [ForeignKey("location_id")]
        [Display(Name = "Entity")]
        public Location Location { get; set; }
        [ForeignKey("job_title_id")]
        [Display(Name = "Role")]
        public JobTitle JobTitle { get; set; }
        public ICollection<Volunteer> Volunteers { get; set; }
        [Display(Name = "Role ID")]
        public int RoleOfferId { get; set; }
        [Display(Name = "Headcount")]

        public int Headcount { get; set; }
    }
}
