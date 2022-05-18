using AutoMapper;
using DomainModels.Models.Entities;

namespace Assignment.Utilities.ServicesUtilities.MapperUtilities
{
    public static class RoleOfferCustomMapper
    {
        public static void MapDbRoleOfferToExcelRoleOfferId
            (ref RoleOffer dbRoleOffer,RoleOffer newExcelRoleOffer
            , IMapper mapper, Venue? dbVenue,JobTitle? dbJobTitle ,
            ExcelEntity? dbExcelEntitiy,FunctionalArea? dbFunctionalArea)
        {
            int dbRoleOfferId = dbRoleOffer.Id;

            int dbFunctionalAreaId = dbRoleOffer.FunctionalArea.Id;
            int dbExcelFunctionalAreaId = dbRoleOffer.FunctionalArea.ExcelFAId;
            int dbJobTitleId = dbRoleOffer.JobTitle.Id;
            int dbExcelJobTitleId = dbRoleOffer.JobTitle.ExcelJTId;
            int dbEntityId = dbRoleOffer.ExcelEntity.Id;
            int dbExcelEntityId = dbRoleOffer.ExcelEntity.ExcelEId;
            int dbVenueId = dbRoleOffer.Venue.Id;
            int dbExcelVenueId = dbRoleOffer.Venue.ExcelVId;

            dbRoleOffer = mapper.Map<RoleOffer>(newExcelRoleOffer);

            dbRoleOffer.Id = dbRoleOfferId;

            if (dbExcelFunctionalAreaId== dbRoleOffer.FunctionalArea.ExcelFAId)
            {
                dbRoleOffer.FunctionalArea.Id = dbFunctionalAreaId;
            }
            else
            {
                dbRoleOffer.FunctionalArea.ExcelFAId= dbRoleOffer
                    .FunctionalArea.ExcelFAId;
               
            }

            if (dbExcelJobTitleId == dbRoleOffer.JobTitle.ExcelJTId)
            {
                dbRoleOffer.JobTitle.Id = dbJobTitleId;
            }
            else
            {
                dbRoleOffer.JobTitle.ExcelJTId= dbRoleOffer
                    .JobTitle.ExcelJTId;
               
            }

            if (dbExcelEntityId == dbRoleOffer.ExcelEntity.ExcelEId)
            {
                dbRoleOffer.ExcelEntity.Id = dbEntityId;
            }
            else
            {
                dbRoleOffer.ExcelEntity.ExcelEId= dbRoleOffer
                    .ExcelEntity.ExcelEId;
               
            }

            if (dbExcelVenueId == dbRoleOffer.Venue.ExcelVId)
            {
                dbRoleOffer.Venue.Id = dbVenueId;
            }
            else
            {
                dbRoleOffer.Venue.ExcelVId= dbRoleOffer
                    .Venue.ExcelVId;
               
            }
        } 
    }
}
