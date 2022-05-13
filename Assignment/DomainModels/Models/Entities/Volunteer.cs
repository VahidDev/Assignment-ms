using DomainModels.Models.Entities.Base;
using DomainModels.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("volunteers")]
    public class Volunteer:Entity
    {
        [Column("role_offer_id")]
        [JsonProperty("role_offer_id")]
        public int? RoleOfferId { get; set; }
        [Column("status")]
        public Statusenum Status { get; set; }
        
    }
}
