using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("job_title_venues")]
    public class JobTitleVenues
    {
        public JobTitle JobTitle { get; set; }
        public Venue Venue { get; set; }
        [Column("job_title_id")]
        public int JobTitleId { get; set; }
        [Column("venue_id")]
        public int VenueId { get; set; }
    }
}
