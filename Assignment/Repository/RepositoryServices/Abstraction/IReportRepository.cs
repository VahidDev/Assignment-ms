using DomainModels.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IReportRepository : IGenericRepository<Report> 
    {
        Task<ICollection<Report>> GetAllAsNoTrackingIncludingItemsAsync
            (Expression<Func<Report, bool>> expression);

        Task<Report> GetByIdIncludingItemsAsNoTrackingAsync(int id);
    }
}
