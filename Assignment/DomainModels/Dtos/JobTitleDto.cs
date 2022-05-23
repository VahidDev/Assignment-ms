using DomainModels.Dtos.Base;
using System.Collections.Generic;

namespace DomainModels.Dtos
{
    public class JobTitleDto: BaseDto
    {
        public string Name { get; set; }
        public ICollection<LocationDto> Locations { get; set; }
    }
}
