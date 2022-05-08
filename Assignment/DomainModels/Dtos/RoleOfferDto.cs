namespace DomainModels.Dtos
{
    public class RoleOfferDto
    {
        public int Id { get; set; }
        public VenueDto Venue { get; set; }
        public FunctionalAreaDto FunctionalArea { get; set; }
        public LocationDto Location { get; set; }
        public JobTitleDto JobTitle { get; set; }
    }
}
