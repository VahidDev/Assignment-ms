namespace DomainModels.Dtos
{
    public class RoleOfferDto:BaseDto
    {
        public FunctionalAreaTypeDto FunctionalAreaType { get; set; }
        public FunctionalAreaDto FunctionalArea { get; set; }
        public JobTitleDto JobTitle { get; set; }
        public LocationDto Location { get; set; }
        public int TotalDemand { get; set; }
        public int RoleOfferFulfillment { get; set; }
        public int WaitlistFulfillment { get; set; }
        public GetFunctionalRequirementDto FunctionalRequirement { get; set; }
    }
}
