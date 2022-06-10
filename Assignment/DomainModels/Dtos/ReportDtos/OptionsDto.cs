using Newtonsoft.Json;
using System.Collections.Generic;

namespace DomainModels.Dtos
{
    public class OptionsDto
    {
        public string Name { get; set; }
        [JsonProperty("value_options")]
        public List<string> ValueOptions { get; set; } = new();
    }
}
