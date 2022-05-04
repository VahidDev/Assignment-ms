using DomainModels.Models.Entities.Base;

namespace DomainModels.Models.Entities
{
    public class RoleOffer : Entity
    {
        public Venue Venue { get; set; }
        public FunctionalArea FunctionalArea { get; set; }
        public Location Location { get; set; }
    }
}
