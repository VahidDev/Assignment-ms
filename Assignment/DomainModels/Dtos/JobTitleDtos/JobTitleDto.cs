using System.Collections.Generic;

namespace DomainModels.Dtos
{
    public class JobTitleDto: BaseDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<LocationDto> Locations { get; set; }
    }
}
