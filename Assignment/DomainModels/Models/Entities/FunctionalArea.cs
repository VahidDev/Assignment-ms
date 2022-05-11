using DomainModels.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainModels.Models.Entities
{
    [Table("functional_areas")]
    public class FunctionalArea : Entity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("code")]
        public string Code { get; set; }
    }
}
