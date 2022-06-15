namespace Assignment.Constants.VolunteerConstants
{
    public static class StatusConstants
    {
        public const string Assigned = "Assigned";
        public const string PreAssigned = "Pre-assigned";
        public const string Pending = "Pending";
        public const string Accepted = "Accepted";
        public const string Confirmed = "Confirmed";
        public const string Complete = "Complete";
        public const string Declined = "Declined";
        public const string Removed = "Removed";
        public const string Expired = "Expired";
        public const string WaitlistOffered = "Waitlist Offered";
        public const string WaitlistAccepted = "Waitlist Accepted";
        public const string WaitlistDeclined = "Waitlist Declined";
        public const string WaitlistAssigned = "Waitlist Assigned";
        public const string NotApproved = "Not Approved";
        
        public static IReadOnlyCollection<string> AssignedNamesList
            = new List<string> { Assigned, PreAssigned, Pending, Accepted, Confirmed };

        public static IReadOnlyCollection<string> WaitlistNamesList
            = new List<string> { WaitlistOffered, WaitlistAccepted, WaitlistAssigned};
    }
}
