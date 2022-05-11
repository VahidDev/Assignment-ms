using DomainModels.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainModels.Models.Entities
{
    [Table("assignment")]
    public class Assignment : Entity
    {
        [Column("volunteer_id")]
        [JsonPropertyName("volunteer_id")]
        public int VolunteerId { get; set; }
        [ForeignKey("role_offer_id")]
        [JsonPropertyName("role_offer_id")]
        public RoleOffer RoleOffer { get; set; }

    }
}
