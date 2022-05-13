using DomainModels.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Utilities.GenericRepositoryUtilities
{
    public static class GenericRepositoryHelper
    {
        public static IQueryable<T> IncludeItemsIfExist<T>
            (this IQueryable<T> querable, IEnumerable<string> includingItems = null) 
            where T : class,IEntity
        {
            if (includingItems != null)
            {
                foreach (string item in includingItems)
                {
                   querable=querable.Include(item);
                }
            }
            return querable;
        }
    }
}
