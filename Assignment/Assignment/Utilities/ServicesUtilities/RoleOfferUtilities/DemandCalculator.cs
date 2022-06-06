namespace Assignment.Utilities.ServicesUtilities.RoleOfferUtilities
{
    public static class DemandCalculator
    {
        public static int CalculateRoleOfferDemand(
            int levelOfConfidence, 
            int totalDemand
            )
        {
            if (levelOfConfidence == 0) levelOfConfidence = 100;
            return (int) (Math.Round((decimal)(levelOfConfidence / 100),2) * totalDemand);
        }
    }
}
