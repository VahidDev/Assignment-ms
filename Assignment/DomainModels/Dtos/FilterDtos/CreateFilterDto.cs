using DomainModels.Dtos.Abstraction;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class CreateFilterDto : IValueToStringConvertible
    {
        [Required]
        [JsonProperty("requirement")]
        public string Requirement { get; set; }
        [Required]
        public string Operator { get; set; }

        [Required]
        public object[] Value { get; set; }
    }
}
