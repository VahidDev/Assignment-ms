using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    public class Role:IdentityRole<int>
    {
        [Key]
        [Column("id")]
        public override int Id { get => base.Id; set => base.Id = value; }
        [Key]
        [Column("name")]
        public override string Name { get => base.Name; set => base.Name = value; }
    }
}
