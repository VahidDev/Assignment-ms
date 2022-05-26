using NpgsqlTypes;

namespace DomainModels.Models.Enums
{
    public enum Statusenum
    {
        [PgName("Assign")]
        Assigned,
        [PgName("Waitlist")]
        Waitlisted,
        [PgName("Free")]
        Free

        //[PgName("Preassigned")]
        //Preassigned,
        //[PgName("Assigned")]
        //Assigned,
        //[PgName("Pending")]
        //Pending,
        //[PgName("Accepted")]
        //Accepted,
        //[PgName("Confirmed")]
        //Confirmed,
        //[PgName("Complete")]
        //Complete,
        //[PgName("Declined")]
        //Declined,
        //[PgName("Removed")]
        //Removed,
        //[PgName("Expired")]
        //Expired,
        //[PgName("WaitlistOffered")]
        //WaitlistOffered,
        //[PgName("WaitlistAccepted")]
        //WaitlistAccepted,
        //[PgName("WaitlistDeclined")]
        //WaitlistDeclined,
        //[PgName("NotApproved")]
        //NotApproved,
        //[PgName("WaitlistAssigned")]
        //WaitlistAssigned
    }
}
