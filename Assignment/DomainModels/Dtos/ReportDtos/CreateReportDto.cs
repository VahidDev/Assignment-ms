using DomainModels.Dtos.Abstraction;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class CreateReportDto 
        : IVolunteerColumnsToStringConvertible
        , IRoleOfferColumnsToStringConvertible
    {
        [Required]
        public string Name { get; set; }
        [JsonProperty("volunteer_columns")]
        public object[] VolunteerColumns { get; set; }
        [JsonProperty("role_offer_columns")]
        public object[] RoleOfferColumns { get; set; }
        [JsonProperty("volunteer_filters")]
        public ICollection<CreateFilterDto> VolunteerFilters { get; set; }
        [JsonProperty("role_offer_filters")]
        public ICollection<CreateFilterDto> RoleOfferFilters { get; set; }
    }
}
