using System;

namespace DomainModels.Models.Entities.Base
{
    public interface IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
