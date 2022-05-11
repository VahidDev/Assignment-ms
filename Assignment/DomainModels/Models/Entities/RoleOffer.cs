using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("role_offers")]
    public class RoleOffer : Entity
    {
        [ForeignKey("venue_id")]
        public Venue Venue { get; set; }
        [ForeignKey("functional_area_id")]
        public FunctionalArea FunctionalArea { get; set; }
        [ForeignKey("location_id")]
        public Location Location { get; set; }
        [ForeignKey("job_title_id")]
        public JobTitle JobTitle { get; set; }
        public ICollection<Volunteer> Volunteers { get; set; }
    }
}
