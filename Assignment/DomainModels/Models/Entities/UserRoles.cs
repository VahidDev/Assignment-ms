using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    public class UserRoles:IdentityUserRole<int>
    {
        [ForeignKey("role_id")]
        public override int RoleId { get => base.RoleId; set => base.RoleId = value; }
        [ForeignKey("user_id")]
        public override int UserId { get => base.UserId; set => base.UserId = value; }
    }
}
