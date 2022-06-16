using AutoMapper;
using DomainModels.Models.Entities;
using DomainModels.Models.Entities.Base;

namespace Assignment.Utilities.ServicesUtilities.MapperUtilities
{
    public static class RoleOfferCustomMapper
    {
        public static RoleOffer MapExcelRoleOfferToDbRoleOffer
            (RoleOffer dbRoleOffer
            , RoleOffer newExcelRoleOffer
            , IReadOnlyCollection<RoleOffer> dbRoleOffers
            , IMapper mapper)
        {
            RoleOffer updatedOrNewRoleOffer = mapper.Map<RoleOffer>(newExcelRoleOffer);
            updatedOrNewRoleOffer.Id = dbRoleOffer.Id;
            updatedOrNewRoleOffer.CreatedAt = dbRoleOffer.CreatedAt;

            // Check if the given excel item is the same as dbRoleOffer's item
            // if yes then just set id of excel item to dbRoleOffer item 
            // if no then find dbItem(if exists) and set this id to it and the same goes for others

            if (dbRoleOffer.FunctionalArea.Code == updatedOrNewRoleOffer.FunctionalArea.Code)
            {
                //updatedOrNewRoleOffer.FunctionalArea.Id = dbRoleOffer.FunctionalArea.Id;
                MapEntityIdAndCreatedAt(updatedOrNewRoleOffer.FunctionalArea, 
                    dbRoleOffer.FunctionalArea.Id, dbRoleOffer.FunctionalArea.CreatedAt);
            }
            else
            {
                updatedOrNewRoleOffer.FunctionalArea 
                    = MapExcelDataToDbDataIfExists
                    (dbRoleOffers,
                    r=>r.FunctionalArea, f=>f.Code==updatedOrNewRoleOffer.FunctionalArea.Code,
                    updatedOrNewRoleOffer.FunctionalArea, mapper);
            }

            if (dbRoleOffer.JobTitle.Code == updatedOrNewRoleOffer.JobTitle.Code)
            {
                MapEntityIdAndCreatedAt(updatedOrNewRoleOffer.JobTitle, 
                    dbRoleOffer.JobTitle.Id, dbRoleOffer.JobTitle.CreatedAt);
            }
            else
            {
                updatedOrNewRoleOffer.JobTitle 
                    = MapExcelDataToDbDataIfExists
                    (dbRoleOffers,
                   r => r.JobTitle, j => j.Code == updatedOrNewRoleOffer.JobTitle.Code,
                   updatedOrNewRoleOffer.JobTitle, mapper);
            }

            if (dbRoleOffer.FunctionalAreaType.Name == updatedOrNewRoleOffer.FunctionalAreaType.Name)
            {
                MapEntityIdAndCreatedAt(updatedOrNewRoleOffer.FunctionalAreaType, 
                    dbRoleOffer.FunctionalAreaType.Id, dbRoleOffer.FunctionalAreaType.CreatedAt);
            }
            else
            {
                updatedOrNewRoleOffer.FunctionalAreaType 
                    = MapExcelDataToDbDataIfExists
                    (dbRoleOffers,
                   r => r.FunctionalAreaType, f => f.Name
                    == updatedOrNewRoleOffer.FunctionalAreaType.Name,
                   updatedOrNewRoleOffer.FunctionalAreaType, mapper);
            }

            if (dbRoleOffer.Location.Code == updatedOrNewRoleOffer.Location.Code)
            {
                MapEntityIdAndCreatedAt(updatedOrNewRoleOffer.Location, 
                    dbRoleOffer.Location.Id, dbRoleOffer.Location.CreatedAt);
            }
            else
            {
                updatedOrNewRoleOffer.Location = MapExcelDataToDbDataIfExists(dbRoleOffers,
                   r => r.Location, l => l.Code == updatedOrNewRoleOffer.Location.Code,
                   updatedOrNewRoleOffer.Location, mapper);
            }
            return updatedOrNewRoleOffer;
        }      
    
        public static U MapExcelDataToDbDataIfExists<T,U>
            (IReadOnlyCollection<T>dbItems
            , Func<T,U>selectCallback
            , Func<U,bool>firstOrDefaultCallback
            , U dbItem
            ,IMapper mapper) 
            where T : IEntity where U : IEntity
        {
            U? item = dbItems
                   .Select(selectCallback)
                   .FirstOrDefault(firstOrDefaultCallback);
            // if this item exists in db then map the fields of updated item to db item
            if (item != null)
            {
                int itemId = item.Id;
                item = mapper.Map<U>(dbItem);
                item.Id = itemId;
                item.CreatedAt = dbItem.CreatedAt;
                return item;
            }
            return dbItem;
        }

        public static void MapEntityIdAndCreatedAt(Entity entity,int id, DateTime? createdAt)
        {
            entity.Id = id;
            entity.CreatedAt = createdAt;
        }
    }
}
