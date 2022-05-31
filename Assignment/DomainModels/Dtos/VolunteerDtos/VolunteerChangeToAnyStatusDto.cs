using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class VolunteerChangeToAnyStatusDto:BaseDto
    {
        [Required]
        public int Id { get; set; }
        [JsonProperty("status")]
        [Required]
        public string Status { get; set; }
    }
}
