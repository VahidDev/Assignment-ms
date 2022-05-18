using DomainModels.Dtos.Base;
using System.Collections.Generic;

namespace DomainModels.Dtos
{
    public class FunctionalAreaDto:BaseDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<JobTitleDto> JobTitles { get; set; }
    }
}
