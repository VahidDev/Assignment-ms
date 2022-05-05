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
        [Column("created_at", TypeName = "timestamp")]
        [JsonProperty("created_date")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("deleted_at", TypeName = "timestamp")]
        [JsonProperty("deleted_date")]
        public DateTime? DeletedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp")]
        [JsonProperty("updated_date")]
        public DateTime? UpdatedAt { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
