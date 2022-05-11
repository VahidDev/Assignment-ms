using DomainModels.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("assignment")]
    public class Assignment : Entity
    {
        [ForeignKey("volunteer_id")]
        public int VolunteerId { get; set; }
        public RoleOffer RoleOffer { get; set; }

    }
}
