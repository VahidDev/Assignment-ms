using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class RoleOfferVolunteerDto 
    {
        [Required]
        [JsonProperty("role_offer_ids")]
        public int[] RoleOfferIds { get; set; }
        [Required]
        public string[] Statuses { get; set; }
        [Required]
        public string[] Locations { get; set; }
        [Required]
        public byte CountryCount { get; set; }
        [Required]
        public AgeRangeDto[] AgeRanges { get; set; }
        [Required]
        public int[] StartingAges { get; set; }
    }
}
