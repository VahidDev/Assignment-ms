using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainModels.Models.Entities
{
    [Table("venues")]
    public class Venue:Entity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("code")]
        public string Code { get; set; }
        public ICollection<RoleOffer> RoleOffers { get; set; }
    }
}
