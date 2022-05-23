using DomainModels.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IFunctionalRequirementRepository
        : IGenericRepository<FunctionalRequirement>
    {
        Task<ICollection<FunctionalRequirement>> GetAllAsNoTrackingIncludingItemsAsync
           (Expression<Func<FunctionalRequirement, bool>> expression);
        Task<FunctionalRequirement> GetByIdAsNoTrackingIncludingItemsAsync
            (Expression<Func<FunctionalRequirement, bool>> expression);
    }
}
