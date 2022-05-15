using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class CreateTemplateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public ICollection<CreateFilterDto> Filters { get; set; }
    }
}
