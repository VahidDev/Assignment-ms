using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("driving_license_types")]
    public class DrivingLicenseType:Entity
    {
        [Column("name")]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
