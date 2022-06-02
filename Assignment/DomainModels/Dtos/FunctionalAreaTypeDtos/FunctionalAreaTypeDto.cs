using System.Collections.Generic;

namespace DomainModels.Dtos
{
    public class FunctionalAreaTypeDto : BaseDto
    {
        public string Name { get; set; }
        public ICollection<FunctionalAreaDto> FunctionalAreas { get; set; }
    }
}
