using DomainModels.Dtos.Base;
using System.Collections.Generic;

namespace DomainModels.Dtos
{
    public class GetTemplateDto:BaseDto
    {
        public string Name { get; set; }
        public ICollection<GetFilterDto> Filters { get; set; }
    }
}
