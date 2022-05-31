using DomainModels.Dtos.Abstraction;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DomainModels.Dtos
{
    public class UpdateReportDto 
        : BaseDto
        , IVolunteerColumnsToStringConvertible
        , IRoleOfferColumnsToStringConvertible
    {
        public string Name { get; set; }
        [JsonProperty("volunteer_columns")]
        public object[] VolunteerColumns { get; set; }
        [JsonProperty("role_offer_columns")]
        public object[] RoleOfferColumns { get; set; }
        [JsonProperty("volunteer_filters")]
        public ICollection<UpdateFilterDto> VolunteerFilters { get; set; }
        [JsonProperty("role_offer_filters")]
        public ICollection<UpdateFilterDto> RoleOfferFilters { get; set; }
    }
}
