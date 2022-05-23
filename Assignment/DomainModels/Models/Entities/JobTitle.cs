using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("job_titles")]
    public class JobTitle:Entity
    {
        [Column("name")]
        [Display(Name= "Role Offer - Job Title")]
        public string Name { get; set; }
        [Column("code")]
        [Display(Name="Role Offer - Job Title Code")]
        public string Code { get; set; }
        public ICollection<Location> Locations { get; set; }
        public ICollection<FunctionalArea> FunctionalAreas { get; set; }
        public ICollection<RoleOffer> RoleOffers { get; set; }
        public ICollection<FunctionalAreaJobTitle> FunctionalAreaJobTitles { get; set; }
        public ICollection<JobTitleLocation> JobTitleVenues { get; set; }

    }
}
