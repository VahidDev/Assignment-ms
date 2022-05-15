using DomainModels.Dtos.Base;
using DomainModels.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class VolunteerChangeToAnyStatusDto:BaseDto
    {
        [JsonProperty("status")]
        [Required]
        public Statusenum Status { get; set; }
    }
}
