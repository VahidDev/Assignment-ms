using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    public class User : IdentityUser<int>
    {
        [Column("id")]
        public override int Id { get => base.Id; set => base.Id = value; }
        [Column("email")]
        public override string Email { get => base.Email; set => base.Email = value; }

        [Column("password_hash")]
        public override string PasswordHash
        { get => base.PasswordHash; set => base.PasswordHash = value; }
        [Column("created_at",TypeName = "timestamp")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("deleted_at", TypeName = "timestamp")]
        public DateTime? DeletedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp")]
        public DateTime? UpdatedAt { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
