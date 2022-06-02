using DomainModels.Models.Entities;
using Repository.Constants;

namespace Assignment.Utilities.ServicesUtilities.ReportUtilities
{
    public static class ReportTemplatesCreator
    {
        public static Template CreateVolunteerTemplateAndMapFilters(ICollection<Filter> filters)
        {
            return new Template
            {
                Name = TemplateDifferentiatorConstants.ReportTemplate
                + nameof(Report.VolunteerTemplate),
                Filters = filters
            };
        }
        public static Template CreateRoleOfferTemplateAndMapFilters(ICollection<Filter> filters)
        {
            return new Template
            {
                Name = TemplateDifferentiatorConstants.ReportTemplate 
                + nameof(Report.RoleOfferTemplate),
                Filters = filters
            };
        }
    }
}
