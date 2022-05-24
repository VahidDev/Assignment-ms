using DomainModels.Dtos.Abstraction;
using Newtonsoft.Json;

namespace DomainModels.Dtos
{
    public class UpdateFilterDto : IValueFromStringConvertible
    {
        public int? Id { get; set; }
        [JsonProperty("requirement_name")]
        public string Requirement { get; set; }
        public string Operator { get; set; }
        public object[] Value { get; set; }
    }
}
