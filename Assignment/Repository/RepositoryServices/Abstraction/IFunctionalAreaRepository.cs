using DomainModels.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IFunctionalAreaRepository:IGenericRepository<FunctionalArea>
    {
        public Task<ICollection<FunctionalArea>> GetAllWithJTsAsNoTrackingAsync
            (Expression<Func<FunctionalArea, bool>> expression);
    }
}
