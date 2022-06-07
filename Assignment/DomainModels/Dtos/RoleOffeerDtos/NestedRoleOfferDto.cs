using Newtonsoft.Json;

namespace DomainModels.Dtos
{
    public class NestedRoleOfferDto:BaseDto
    {
        public int TotalDemand { get; set; }
        [JsonProperty("assignee_demand")]
        public int AssigneeDemand { get; set; }
        [JsonProperty("waitlist_demand")]
        public int WaitlistDemand { get; set; }
        [JsonProperty("level_of_confidence")]
        public int? LevelOfConfidence { get; set; }
        public GetFunctionalRequirementDto FunctionalRequirement { get; set; }
        public int OverallAssigned { get; set; }
        public int AssigneeDemandPercentage { get; set; }
        public int WaitlistDemandPercentage { get; set; }
        public int OverallWaitlisted { get; set; }
        [JsonProperty("role_offer_id")]
        public int RoleOfferId { get; set; }
    }
}
