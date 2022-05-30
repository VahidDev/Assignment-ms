using DomainModels.Dtos.Abstraction;
using Newtonsoft.Json;

namespace DomainModels.Dtos
{
    public class UpdateRequirementDto : IValueToArrayConvertible
    {
        public int? Id { get; set; }
        [JsonProperty("requirement_name")]
        public string RequirementName { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }
    }
}
