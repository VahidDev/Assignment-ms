using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("job_title_locations")]
    public class JobTitleLocation
    {
        public JobTitle JobTitle { get; set; }
        public Location Location { get; set; }
        [Column("job_title_id")]
        public int JobTitleId { get; set; }
        [Column("location_id")]
        public int LocationId { get; set; }
    }
}
