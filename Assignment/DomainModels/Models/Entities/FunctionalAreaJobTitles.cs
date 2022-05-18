using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("functional_area_job_titles")]
    public class FunctionalAreaJobTitles
    {
        public FunctionalArea FunctionalArea { get; set; }
        public JobTitle JobTitle { get; set; }
        [Column("functional_area_id")]
        public int FunctionalAreaId { get; set; }
        [Column("job_title_id")]
        public int JobTitleId { get; set; }
    }
}
