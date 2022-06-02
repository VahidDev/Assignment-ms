﻿namespace Assignment.Constants.VolunteerConstants
{
    public static class StatusConstants
    {
        public const string Assigned = "Assigned";
        public const string PreAssigned = "Preassigned";
        public const string Pending = "Pending";
        public const string Accepted = "Accepted";
        public const string Confirmed = "Confirmed";
        public const string Complete = "Complete";
        public const string Declined = "Declined";
        public const string Removed = "Removed";
        public const string Expired = "Expired";
        public const string WaitlistOffered = "WaitlistOffered";
        public const string WaitlistAccepted = "WaitlistAccepted";
        public const string WaitlistDeclined = "WaitlistDeclined";
        public const string WaitlistAssigned = "WaitlistAssigned";
        public const string NotApproved = "NotApproved";
        
        public static IReadOnlyCollection<string> AssignedNamesList
            = new List<string> { Assigned, PreAssigned, Pending, Accepted };

        public static IReadOnlyCollection<string> WaitlistNamesList
            = new List<string> { WaitlistOffered, WaitlistAccepted, WaitlistAssigned};
    }
}
