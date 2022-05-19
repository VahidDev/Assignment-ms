using DomainModels.Dtos.Base;
using System.Collections.Generic;

namespace DomainModels.Dtos
{
    public class VenueDto : BaseDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public NestedRoleOfferDto RoleOffer { get; set; }
    }
}
