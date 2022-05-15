using DomainModels.Dtos.Base;
using DomainModels.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class VolunteerDto:BaseDto
    {
        [Required]
        [JsonProperty("role_offer_id")]
        public int RoleOfferId { get; set; }
        [Required]
        [JsonProperty("status")]
        public Statusenum Status { get; set; }
    }
}
