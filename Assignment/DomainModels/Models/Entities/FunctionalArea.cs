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
        [Display(Name = "Role Offer - Functional Area")]
        public string Name { get; set; }
        [Display(Name = "Role Offer - Functional Area Code")]
        [Column("code")]
        public string Code { get; set; }
        public ICollection<FunctionalAreaType> FunctionalAreaTypes { get; set; }
        public ICollection<JobTitle> JobTitles { get; set; }
        public ICollection<RoleOffer> RoleOffers { get; set; }
        public ICollection<FunctionalAreaTypeFunctionalArea> ExcelEntityFunctionalAreas { get; set; }
        public ICollection<FunctionalAreaJobTitle> FunctionalAreaJobTitles { get; set; }

    }
}
