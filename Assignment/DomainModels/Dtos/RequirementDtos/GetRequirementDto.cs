using DomainModels.Dtos.Abstraction;
using Newtonsoft.Json;

namespace DomainModels.Dtos
{
    public class GetRequirementDto : BaseDto, IValueToStringConvertible
    {
        [JsonProperty("requirement_name")]
        public string RequirementName { get; set; }
        public string Operator { get; set; }
        public object[] Value { get; set; }
    }
}
