using DomainModels.Dtos.Base;
using System.Collections.Generic;

namespace DomainModels.Dtos
{
    public class ExcelEntityDto : BaseDto
    {
        public string Name { get; set; }
        public ICollection<FunctionalAreaDto> FunctionalAreas { get; set; }
    }
}
