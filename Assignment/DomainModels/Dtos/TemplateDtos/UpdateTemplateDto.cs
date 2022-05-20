using DomainModels.Dtos.Base;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModels.Dtos
{
    public class UpdateTemplateDto:BaseDto
    {
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<UpdateFilterDto> Filters { get; set; }
    }
}
