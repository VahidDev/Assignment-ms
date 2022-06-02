using System.Collections.Generic;

namespace DomainModels.Dtos.DashboardDtos
{
    public class GetVolunteerInfoDashboardDto
    {
        public int OverallMales { get; set; }
        public int OverallFemales { get; set; }
        public int OverallInternationals { get; set; }
        public int OverallLocals { get; set; }
        public List<CountryNameDto> CountryNameDtos { get; set; } 
            = new List<CountryNameDto>();
        public int Others { get; set; }
        public ICollection<AgeRangeCountDto> AgeRanges { get; set; }
         = new List<AgeRangeCountDto>();
        public ICollection<StartingAgeCountDto> StartingAges { get; set; }
        = new List<StartingAgeCountDto>();
    }
}
