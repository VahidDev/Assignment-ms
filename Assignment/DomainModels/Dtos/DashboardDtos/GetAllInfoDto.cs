namespace DomainModels.Dtos
{
    public class GetAllInfoDto
    {
        public double Assigned { get; set; }
        public double PreAssigned { get; set; }
        public double Pending {get;set;}
        public double Accepted {get;set;}
        public double WaitlistOffered {get;set;}
        public double WaitlistAccepted { get; set; }
        public double WaitlistAssigned {get;set;}
        public double AssignedRest { get; set; }
        public double WaitlistRest { get; set; }
        public int TotalAssigned { get; set; }
        public int TotalWaitlisted { get; set; }
        public int OverallAssigneeDemand { get; set; }
        public int OverallNoneAssigned { get; set; }
        public int OverallWaitlistDemand { get; set; }
        public int OverallNoneWaitlisted { get; set; }
    }
}
