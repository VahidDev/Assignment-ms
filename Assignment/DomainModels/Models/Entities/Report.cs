using DomainModels.Dtos.Abstraction;
using DomainModels.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("reports")]
    public class Report 
        : Entity, 
        IRoleOfferColumnsToArrayConvertible, 
        IVolunteerColumnsToArrayConvertible
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("volunteer_columns")]
        public string VolunteerColumns { get; set; }
        [Column("role_offer_columns")]
        public string RoleOfferColumns { get; set; }
        [ForeignKey("role_offer_template_id")]
        public Template RoleOfferTemplate { get; set; }
        [ForeignKey("volunteer_template_id")]
        public Template VolunteerTemplate { get; set; }
    }
}
