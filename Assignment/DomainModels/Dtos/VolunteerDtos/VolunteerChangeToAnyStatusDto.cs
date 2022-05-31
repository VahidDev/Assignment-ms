using Newtonsoft.Json;

namespace DomainModels.Dtos
{
    public class VolunteerChangeToAnyStatusDto:BaseDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
