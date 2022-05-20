using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("functional_area_type_functional_areas")]
    public class FunctionalAreaTypeFunctionalArea
    {
        public FunctionalAreaType FunctionalAreaType { get; set; }
        public FunctionalArea FunctionalArea { get; set; }
        [Column("functional_area_id")]
        public int FunctionalAreaId { get; set; }
        [Column("functional_area_type_id")]
        public int FunctionalAreaTypeId { get; set; }
    }
}
