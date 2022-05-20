using DomainModels.Dtos.Base;
namespace DomainModels.Dtos
{
    public class NestedRoleOfferDto:BaseDto
    {
        public int TotalDemand { get; set; }
        public int RoleOfferFulfillment { get; set; }
        public int WaitlistFulfillment { get; set; }
    }
}
