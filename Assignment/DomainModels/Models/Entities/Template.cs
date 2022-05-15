using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("template")]
    public class Template:Entity
    {
        [Column("name")]
        public string Name { get; set; }
        public ICollection<Filter> Filters { get; set; }
    }
}
