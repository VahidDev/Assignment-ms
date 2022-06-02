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
        [JsonProperty("level_of_confidence")]
        public int? LevelOfConfidence { get; set; }
        [JsonProperty("waitlist_count")]
        public int? WaitlistCount { get; set; }
        public GetFunctionalRequirementDto FunctionalRequirement { get; set; }
        public int OverallAssigned { get; set; }
        public int AssigneeDemand { get; set; }
        public int WaitlistDemand { get; set; }
        public int OverallWaitlisted { get; set; }
    }
}
