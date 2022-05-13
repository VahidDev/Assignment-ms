using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("venues")]
    public class Venue:Entity
    {
        [Column("name")]
        [Display(Name = "Venue")]
        public string Name { get; set; }
        [Column("code")]
        public string Code { get; set; }
        public ICollection<RoleOffer> RoleOffers { get; set; }
    }
}
