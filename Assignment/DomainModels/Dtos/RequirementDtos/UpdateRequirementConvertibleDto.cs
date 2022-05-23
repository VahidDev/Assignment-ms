using DomainModels.Dtos.Abstraction;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class UpdateRequirementConvertibleDto  : IValueFromStringConvertible
    {
        public int? Id { get; set; }
        [Required]
        [JsonProperty("requirement_name")]
        public string RequirementName { get; set; }
        [Required]
        public string Operator { get; set; }
        [Required]
        public object[] Value { get; set; }
    }
}
