using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos.RoleOffeerDtos
{
    public class ImportRoleOfferDetailsDto 
    {
        [Display(Name = "Role Offer ID")]
        public int RoleOfferId { get; set; }
        [Display(Name ="Level Of Confidence")]
        public int LevelOfConfidence { get; set; }
        [Display(Name ="Waitlist Count")]
        public int WaitlistCount { get; set; }
        [Display(Name = "Total Demand")]
        public int TotalDemand { get; set; }
    }
}
