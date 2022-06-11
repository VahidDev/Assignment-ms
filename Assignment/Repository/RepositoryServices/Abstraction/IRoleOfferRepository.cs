using DomainModels.Dtos;
using DomainModels.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IRoleOfferRepository
        :IGenericRepository<RoleOffer>
    {
        Task<RoleOffer> FirstOrDefaultIncludingItemsAsync
            (Expression<Func<RoleOffer,bool>>expression);
        Task<ICollection<RoleOffer>> GetAllAsNoTrackingIncludingItemsAsync
            (Expression<Func<RoleOffer, bool>> expression);
        Task<ICollection<RoleOffer>> GetAllIncludingItemsAsync();
        Task<ICollection<RoleOffer>> GetAllSpecificRoleOffersAsNoTrackingAsync
            (Expression<Func<RoleOffer, bool>> expression);
        Task<ICollection<RoleOffer>> GetAllSpecificRoleOffersAsync
            (Expression<Func<RoleOffer, bool>> expression);
        Task<ICollection<AssigneeDemandWaitlistCountDto>> GetAllAssigneeDemandWaitlistCountsAsync
            (Expression<Func<RoleOffer, bool>> expression);
    }
}
