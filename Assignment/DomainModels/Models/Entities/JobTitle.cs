using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("job_titles")]
    public class JobTitle:Entity
    {
        [Column("name")]
        [Display(Name= "Role")]
        public string Name { get; set; }
        [Column("code")]
        public string Code { get; set; }
        public ICollection<RoleOffer> RoleOffers { get; set; }
        
    }
}
