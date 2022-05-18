using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("functional_areas")]
    public class FunctionalArea : Entity
    {
        [Column("name")]
        [Display(Name = "Project")]
        public string Name { get; set; }
        [Column("code")]
        public string Code { get; set; }
        public ICollection<ExcelEntity> ExcelEntities { get; set; }
        [Display(Name = "FA ID")]
        [Column("functional_area_id")]
        public int ExcelFAId { get; set; }
        public ICollection<JobTitle> JobTitles { get; set; }
        public ICollection<RoleOffer> RoleOffers { get; set; }
        public ICollection<ExcelEntityFunctionalAreas> ExcelEntityFunctionalAreas { get; set; }
        public ICollection<FunctionalAreaJobTitles> FunctionalAreaJobTitles { get; set; }

    }
}
