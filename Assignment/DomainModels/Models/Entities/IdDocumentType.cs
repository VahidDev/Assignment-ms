using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("id_document_types")]
    public class IdDocumentType:Entity
    {
        [Column("name")]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
