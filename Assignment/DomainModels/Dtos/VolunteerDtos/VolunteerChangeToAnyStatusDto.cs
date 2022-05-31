using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class VolunteerChangeToAnyStatusDto:BaseDto
    {
        [Required]
        [JsonProperty("candidate_id")]
        public int CandidateId { get; set; }
        [JsonProperty("status")]
        [Required]
        public string Status { get; set; }
    }
}
