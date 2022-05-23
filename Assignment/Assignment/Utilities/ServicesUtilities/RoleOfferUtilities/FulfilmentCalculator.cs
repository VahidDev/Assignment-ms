namespace Assignment.Utilities.ServicesUtilities.RoleOfferUtilities
{
    public static class FulfilmentCalculator
    {
        public static int CalculateRoleFulfilment(int levelOfConfidence, int totalDemand)
        {
            if (levelOfConfidence == 0) levelOfConfidence = 100;
            return (totalDemand * 100) / levelOfConfidence;
        }
        public static int CalculateWaitlistFulfilment(int waitlistCount, int totalDemand)
        {
            if(waitlistCount == 0) return 0;
            return (totalDemand * 100) / waitlistCount;
        }
    }
}
