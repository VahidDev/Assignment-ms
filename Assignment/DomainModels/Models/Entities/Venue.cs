using DomainModels.Models.Entities.Base;
using System.Collections.Generic;

namespace DomainModels.Models.Entities
{
    public class Venue:Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<RoleOffer> RoleOffers { get; set; }
    }
}
