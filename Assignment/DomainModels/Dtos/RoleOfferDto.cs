using DomainModels.Dtos.Base;

namespace DomainModels.Dtos
{
    public class RoleOfferDto : BaseDto
    {
        public VenueDto Venue { get; set; }
        public FunctionalAreaDto FunctionalArea { get; set; }
        public LocationDto Location { get; set; }
        public JobTitleDto JobTitle { get; set; }
    }
}
