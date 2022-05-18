using DomainModels.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IExcelEntityRepository:IGenericRepository<ExcelEntity>
    {
        public Task<ICollection<ExcelEntity>> GetAllAsNoTrackingIncludingItemsAsync
          (Expression<Func<ExcelEntity, bool>> expression);
    }
}
