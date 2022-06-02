namespace DomainModels.Dtos
{
    public class GetRoleOfferDashboardDto
    {
        public GetFunctionalAreaTypeNameDto FunctionalAreaType { get; set; }
        public GetFunctionalAreaNameDto FunctionalArea { get; set; }
        public GetJobTitleNameDto JobTitle { get; set; }
        public GetLocationNameDto Location { get; set; }
        public int TotalDemand { get; set; }
        public int RoleOfferFulfillment { get; set; }
        public int WaitlistFulfillment { get; set; }
        public int Assigned { get; set;}
        public int PreAssigned {get;set;}
        public int Pending {get;set;}
        public int Accepted {get;set;}
        public int WaitlistOffered {get;set;}
        public int WaitlistAccepted {get;set;}
        public int WaitlistAssigned {get;set;}
    }
}
