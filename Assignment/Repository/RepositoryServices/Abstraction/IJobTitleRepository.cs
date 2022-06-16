using DomainModels.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IJobTitleRepository:IGenericRepository<JobTitle>
    {
        public Task<ICollection<JobTitle>> GetAllWithLocsAsNoTrackingAsync
            (Expression<Func<JobTitle, bool>> expression);
    }
}
