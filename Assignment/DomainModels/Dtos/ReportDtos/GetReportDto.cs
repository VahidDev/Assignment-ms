using Newtonsoft.Json;
using System.Collections.Generic;

namespace DomainModels.Dtos
{
    public class GetReportDto : BaseDto
    {
        public string Name { get; set; }
        [JsonProperty("volunteer_columns")]
        public object[] VolunteerColumns { get; set; }
        [JsonProperty("role_offer_columns")]
        public object[] RoleOfferColumns { get; set; }
        [JsonProperty("volunteer_filters")]
        public ICollection<GetFilterDto> VolunteerFilters { get; set; }
        [JsonProperty("role_offer_columns")]
        public ICollection<GetFilterDto> RoleOfferFilters { get; set; }
    }
}
