using Assignment.Utilities.ServicesUtilities.ReportUtilities;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;

namespace Assignment.Utilities.ServicesUtilities.MapperUtilities
{
    public static class UpdateReportMapper
    {
        public static Report MapToReport
            (UpdateReportDto updateReportDto
            , Report dbReport
            , Report updatedReport
            , IMapper mapper)
        {
            if (dbReport.RoleOfferTemplate != null)
            {
                updatedReport.RoleOfferTemplate = new();
                updatedReport.RoleOfferTemplate.Id = dbReport.RoleOfferTemplate.Id;
                updatedReport.RoleOfferTemplate.Name = dbReport.RoleOfferTemplate.Name;
                if (updateReportDto.RoleOfferFilters != null)
                {
                    updatedReport.RoleOfferTemplate.Filters
                        = mapper.Map<ICollection<Filter>>(updateReportDto.RoleOfferFilters);
                }
            }
            else
            {
                if (updateReportDto.RoleOfferFilters != null)
                {
                    updatedReport.RoleOfferTemplate = ReportTemplatesCreator
                        .CreateRoleOfferTemplateAndMapFilters
                        (mapper.Map<ICollection<Filter>>(updateReportDto.RoleOfferFilters));
                }
            }
            if (dbReport.VolunteerTemplate != null)
            {
                updatedReport.VolunteerTemplate = new();
                updatedReport.VolunteerTemplate.Id = dbReport.VolunteerTemplate.Id;
                updatedReport.VolunteerTemplate.Name = dbReport.VolunteerTemplate.Name;
                if (updateReportDto.VolunteerFilters != null)
                {
                    updatedReport.VolunteerTemplate.Filters
                        = mapper.Map<ICollection<Filter>>(updateReportDto.VolunteerFilters);
                }
            }
            else
            {
                if (updateReportDto.VolunteerFilters != null)
                {
                    updatedReport.VolunteerTemplate = ReportTemplatesCreator
                      .CreateVolunteerTemplateAndMapFilters
                      (mapper.Map<ICollection<Filter>>(updateReportDto.VolunteerFilters));
                }
            }
            return updatedReport;
        }
    }
}
