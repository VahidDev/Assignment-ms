﻿using DomainModels.Models.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("locations")]
    public class Location:Entity
    {
        [Column("name")]
        [Display(Name = "Role Offer - Location")]
        public string Name { get; set; }
        [Column("Role Offer - Location Code")]
        public string Code { get; set; }
        public ICollection<JobTitle> JobTitles { get; set; }
        public ICollection<RoleOffer> RoleOffers { get; set; }
        public ICollection<JobTitleLocation> JobTitleVenues { get; set; }
    }
}
