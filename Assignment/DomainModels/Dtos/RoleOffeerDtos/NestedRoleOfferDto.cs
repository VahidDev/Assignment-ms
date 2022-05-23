using DomainModels.Dtos.Base;
using Newtonsoft.Json;

namespace DomainModels.Dtos
{
    public class NestedRoleOfferDto:BaseDto
    {
        public int TotalDemand { get; set; }
        [JsonProperty("role_offer_fulfillment")]
        public int RoleOfferFulfillment { get; set; }
        [JsonProperty("waitlist_fulfillment")]
        public int WaitlistFulfillment { get; set; }
        public GetFunctionalRequirementDto FunctionalRequirement { get; set; }
    }
}
