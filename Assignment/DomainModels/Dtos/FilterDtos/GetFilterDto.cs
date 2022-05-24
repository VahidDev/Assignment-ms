using Newtonsoft.Json;

namespace DomainModels.Dtos
{
    public class GetFilterDto : BaseDto
    {
        [JsonProperty("requirement_name")]
        public string Requirement { get; set; }
        public string Operator { get; set; }
        public object[] Value { get; set; }
    }
}
