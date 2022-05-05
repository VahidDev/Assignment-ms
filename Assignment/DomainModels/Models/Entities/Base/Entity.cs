using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities.Base
{
    public class Entity : IEntity
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Column("deleted_date")]
        public DateTime DeletedDate { get; set; }
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
