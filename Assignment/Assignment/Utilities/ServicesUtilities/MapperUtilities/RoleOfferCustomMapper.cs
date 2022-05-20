using AutoMapper;
using DomainModels.Models.Entities;

namespace Assignment.Utilities.ServicesUtilities.MapperUtilities
{
    public static class RoleOfferCustomMapper
    {
        public static void MapDbRoleOfferToExcelRoleOfferId
            (ref RoleOffer dbRoleOffer,RoleOffer newExcelRoleOffer
            , IMapper mapper, Location? dbVenue,JobTitle? dbJobTitle ,
            FunctionalAreaType? dbExcelEntitiy,FunctionalArea? dbFunctionalArea)
        {
            int dbRoleOfferId = dbRoleOffer.Id;

            int dbFunctionalAreaId = dbRoleOffer.FunctionalArea.Id;
            string dbExcelFunctionalAreaId = dbRoleOffer.FunctionalArea.Code;
            int dbJobTitleId = dbRoleOffer.JobTitle.Id;
            string dbExcelJobTitleId = dbRoleOffer.JobTitle.Code;
            int dbEntityId = dbRoleOffer.FunctionalAreaType.Id;
            string dbExcelEntityId = dbRoleOffer.FunctionalAreaType.Name;
            int dbVenueId = dbRoleOffer.Location.Id;
            string dbExcelVenueId = dbRoleOffer.Location.Code;

            dbRoleOffer = mapper.Map<RoleOffer>(newExcelRoleOffer);

            dbRoleOffer.Id = dbRoleOfferId;

            if (dbExcelFunctionalAreaId== dbRoleOffer.FunctionalArea.Name)
            {
                dbRoleOffer.FunctionalArea.Id = dbFunctionalAreaId;
            }
            else
            {
                dbRoleOffer.FunctionalArea.Code= dbRoleOffer.FunctionalArea.Code;
            }
            if (dbExcelJobTitleId == dbRoleOffer.JobTitle.Code)
            {
                dbRoleOffer.JobTitle.Id = dbJobTitleId;
            }
            else
            {
                dbRoleOffer.JobTitle.Code = dbRoleOffer.JobTitle.Code;
            }
            if (dbExcelEntityId == dbRoleOffer.FunctionalAreaType.Name)
            {
                dbRoleOffer.FunctionalAreaType.Id = dbEntityId;
            }
            else
            {
                dbRoleOffer.FunctionalAreaType.Name = dbRoleOffer.FunctionalAreaType.Name;
               
            }
            if (dbExcelVenueId == dbRoleOffer.Location.Code)
            {
                dbRoleOffer.Location.Id = dbVenueId;
            }
            else
            {
                dbRoleOffer.Location.Code = dbRoleOffer.Location.Code;
            }
        } 
    }
}
