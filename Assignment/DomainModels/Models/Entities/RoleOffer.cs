using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("role_offers")]
    public class RoleOffer : Entity
    {
        [Display(Name = "Custom Object ExcelEntity")]
        [ForeignKey("excel_entity_id")]
        public ExcelEntity ExcelEntity { get; set; }
        [ForeignKey("functional_area_id")]
        [Display(Name = "Custom Object Project")]
        public FunctionalArea FunctionalArea { get; set; }
        [ForeignKey("job_title_id")]
        [Display(Name = "Custom Object Role")]
        public JobTitle JobTitle { get; set; }
        [ForeignKey("venue_id")]
        [Display(Name = "Custom Object Venue")]
        public Venue Venue { get; set; }
        [Display(Name = "Role ID")]
        [Column("role_offer_id")]
        public int RoleOfferId { get; set; }
        [Display(Name = "Headcount")]
        [Column("headcount")]
        public int Headcount { get; set; }
        public ICollection<Volunteer> Volunteers { get; set; }
    }
}
