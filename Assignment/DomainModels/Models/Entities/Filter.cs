using DomainModels.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("filters")]
    public class Filter:Entity
    {
        [Column("requirement")]
        public string Requirement { get; set; }
        [Column("operator")]
        public string Operator { get; set; }
        [Column("value")]
        public string Value { get; set; }
        [ForeignKey("template_id")]
        public Template Template { get; set; }
    }
}
