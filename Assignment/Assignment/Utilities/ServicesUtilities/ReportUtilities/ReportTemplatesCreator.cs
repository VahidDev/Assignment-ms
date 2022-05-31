using DomainModels.Models.Entities;

namespace Assignment.Utilities.ServicesUtilities.ReportUtilities
{
    public static class ReportTemplatesCreator
    {
        public static Template CreateVolunteerTemplateAndMapFilters(ICollection<Filter> filters)
        {
            return new Template
            {
                Name = "Report " + nameof(Report.VolunteerTemplate),
                Filters = filters
            };
        }
        public static Template CreateRoleOfferTemplateAndMapFilters(ICollection<Filter> filters)
        {
            return new Template
            {
                Name = "Report " + nameof(Report.RoleOfferTemplate),
                Filters = filters
            };
        }
    }
}
