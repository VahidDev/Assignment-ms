using DomainModels.Dtos.Base;
using DomainModels.Models.Enums;
using Newtonsoft.Json;

namespace DomainModels.Dtos
{
    public class VolunteerDto:BaseDto
    {
        [JsonProperty("role_offer_id")]
        public int RoleOfferId { get; set; }
        [JsonProperty("volunteer_id")]
        public int VolunteerId { get; set; }
        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}
