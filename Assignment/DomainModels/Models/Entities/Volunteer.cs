using DomainModels.Models.Entities.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
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
        public string Status { get; set; }
        [Column("international_volunteer")]
        public string InternationalVolunteer { get; set; }
        [Column("age")]
        public int Age { get; set; }
        [Column("residence_country")]
        public string Country { get; set; }
        [Column("gender")]
        public string Gender { get; set; }
        public ICollection<History> Histories { get; set; }
    }
}
