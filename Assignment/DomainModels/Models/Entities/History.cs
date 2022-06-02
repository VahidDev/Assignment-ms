using DomainModels.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;
namespace DomainModels.Models.Entities
{
    [Table("histories")]
    public class History : Entity
    {
        public Volunteer Volunteer { get; set; }
        [Column("user_id")]
        public int VolunteerId { get; set; }
        public RoleOffer RoleOffer { get; set; }
        [Column("role_offer_id")]
        public int? RoleOfferId { get; set; }
        [Column("status")]
        public string Status { get; set; }
    }
}
