using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class AssignOrWaitlistVolunteerDto
    {
        [Required]
        [JsonProperty("candidate_id")]
        public int CandidateId { get; set; }
        [Required]
        [JsonProperty("role_offer_id")]
        public int RoleOfferId { get; set; }
        [Required]
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
