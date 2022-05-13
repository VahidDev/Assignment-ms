using DomainModels.Dtos.Base;
using DomainModels.Models.Enums;
using Newtonsoft.Json;

namespace DomainModels.Dtos
{
    public class VolunteerDto:BaseDto
    {
        [JsonProperty("role_offer_id")]
        public int RoleOfferId { get; set; }
        [JsonProperty("status")]
        public Statusenum Status { get; set; }
    }
}
