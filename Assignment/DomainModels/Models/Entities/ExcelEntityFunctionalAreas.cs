using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("entity_functional_areas")]
    public class ExcelEntityFunctionalAreas
    {
        public ExcelEntity ExcelEntity { get; set; }
        public FunctionalArea FunctionalArea { get; set; }
        [Column("functional_area_id")]
        public int FunctionalAreaId { get; set; }
        [Column("entity_id")]
        public int ExcelEntityId { get; set; }
    }
}
