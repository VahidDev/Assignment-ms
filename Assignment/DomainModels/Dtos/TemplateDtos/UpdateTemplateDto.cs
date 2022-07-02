using System.Collections.Generic;

namespace DomainModels.Dtos
{
    public class UpdateTemplateDto : BaseDto
    {
        public string Name { get; set; }
        public ICollection<UpdateFilterDto> Filters { get; set; }
    }
}
