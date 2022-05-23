using System.Collections.Generic;

namespace DomainModels.Dtos
{
    public class GetFunctionalRequirementDto : BaseDto
    {
        public ICollection<GetRequirementDto> Requirements { get; set; }
    }
}
