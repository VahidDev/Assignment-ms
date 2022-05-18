using DomainModels.Dtos.Base;

namespace DomainModels.Dtos
{
    public class RoleOfferDto:BaseDto
    {
        public ExcelEntityDto ExcelEntity { get; set; }
        public FunctionalAreaDto FunctionalArea { get; set; }
        public JobTitleDto JobTitle { get; set; }
        public VenueDto Venue { get; set; }
        public int Headcount { get; set; }
    }
}
