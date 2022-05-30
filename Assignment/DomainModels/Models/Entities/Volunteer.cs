using DomainModels.Models.Entities.Base;
using DomainModels.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("volunteers")]
    public class Volunteer:Entity
    {
        [Key]
        [Column("candidate_id")]
        [JsonProperty("candidate_id")]
        public int CandidateId { get; set; }
        [Column("role_offer_id")]
        [JsonProperty("role_offer_id")]
        public int? RoleOfferId { get; set; }
        [Column("status")]
        public Statusenum Status { get; set; }
        
    }
}
