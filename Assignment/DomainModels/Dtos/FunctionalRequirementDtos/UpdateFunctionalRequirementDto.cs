using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class UpdateFunctionalRequirementDto
    {
        public int? Id { get; set; }
        public ICollection<UpdateRequirementDto> Requirements { get; set; }
        [JsonProperty("level_of_confidence")]
        public int LevelOfConfidence { get; set; } = 100;
        [JsonProperty("waitlist_demand")]
        public int WaitlistDemand { get; set; } = 0;
        [Required]
        [JsonProperty("total_demand")]
        public int TotalDemand { get; set; }
        [Required]
        [JsonProperty("role_offer_id")]
        public int RoleOfferId { get; set; }
    }
}
