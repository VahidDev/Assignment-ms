using DomainModels.Dtos.Abstraction;
using DomainModels.Dtos.Base;
using Newtonsoft.Json;

namespace DomainModels.Dtos
{
    public class GetRequirementDto : BaseDto, IValueFromStringConvertible
    {
        [JsonProperty("requirement_name")]
        public string RequirementName { get; set; }
        public string Operator { get; set; }
        public object[] Value { get; set; }
    }
}
