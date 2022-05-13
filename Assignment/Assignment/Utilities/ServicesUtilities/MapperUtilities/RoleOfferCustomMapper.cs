using AutoMapper;
using DomainModels.Models.Entities;

namespace Assignment.Utilities.ServicesUtilities.MapperUtilities
{
    public static class RoleOfferCustomMapper
    {
        public static void MapDbRoleOfferIdsToExcelRoleOfferIds
            (ref RoleOffer dbRoleOffer,RoleOffer newExcelRoleOffer, IMapper mapper)
        {
            int dbRoleOfferId = dbRoleOffer.Id;

            int functionalAreaId = dbRoleOffer.FunctionalArea.Id;
            int jobTitleId = dbRoleOffer.JobTitle.Id;
            int locationId = dbRoleOffer.Location.Id;
            int venueId = dbRoleOffer.Venue.Id;

            dbRoleOffer = mapper.Map<RoleOffer>(newExcelRoleOffer);

            dbRoleOffer.Id = dbRoleOfferId;
            dbRoleOffer.FunctionalArea.Id = functionalAreaId;
            dbRoleOffer.JobTitle.Id = jobTitleId;
            dbRoleOffer.Location.Id = locationId;
            dbRoleOffer.Venue.Id = venueId;
        } 
    }
}
