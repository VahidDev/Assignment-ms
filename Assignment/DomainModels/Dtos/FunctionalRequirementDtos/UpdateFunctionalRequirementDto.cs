using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class UpdateFunctionalRequirementDto:BaseDto
    {
        public ICollection<UpdateRequirementDto> Requirements { get; set; }
        public int LevelOfConfidence { get; set; } = 100;
        public int WaitlistCount { get; set; } = 0;
        [Required]
        public int TotalDemand { get; set; }
        [Required]
        public int RoleOfferId { get; set; }
    }
}
