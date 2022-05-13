using DomainModels.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainModels.Models.Entities
{
    [Table("functional_areas")]
    public class FunctionalArea : Entity
    {
        [Column("name")]
        [Display(Name = "Project")]
        public string Name { get; set; }
        [Column("code")]
        public string Code { get; set; }
    }
}
