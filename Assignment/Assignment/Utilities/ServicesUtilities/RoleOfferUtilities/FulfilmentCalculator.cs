namespace Assignment.Utilities.ServicesUtilities.RoleOfferUtilities
{
    public static class FulfilmentCalculator
    {
        public static int CalculateRoleFulfilment(int levelOfConfidence, int totalDemand)
        {
            if (levelOfConfidence == 0) levelOfConfidence = 100;
            return (int) (Math.Round((decimal)(levelOfConfidence / 100),2) * totalDemand);
        }
        public static int CalculateWaitlistFulfilment(int waitlistCount)
        {
            return waitlistCount;
        }
    }
}
