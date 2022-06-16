using DomainModels.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IFunctionalAreaTypeRepository:IGenericRepository<FunctionalAreaType>
    {
        public Task<ICollection<FunctionalAreaType>> GetAllWithItemsAsNoTrackingAsync
          (Expression<Func<FunctionalAreaType, bool>> expression);

        public Task<ICollection<FunctionalAreaType>> GetAllWithFAsAsNoTrackingAsync
            (Expression<Func<FunctionalAreaType, bool>> expression);
    }
}
