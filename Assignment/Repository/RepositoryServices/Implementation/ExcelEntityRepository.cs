﻿using DomainModels.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Implementation
{
    internal class ExcelEntityRepository:GenericRepository<ExcelEntity>,IExcelEntityRepository
    {
        public ExcelEntityRepository
           (AppDbContext context, ILogger logger) : base(context, logger) { }
        public async Task<ICollection<ExcelEntity>> GetAllAsNoTrackingIncludingItemsAsync
           (Expression<Func<ExcelEntity, bool>> expression)
        {
            return await dbSet
                .Include(r => r.FunctionalAreas.Where(r => !r.IsDeleted))
                .ThenInclude(f => f.JobTitles.Where(r => !r.IsDeleted))
                .ThenInclude(j => j.Venues.Where(r => !r.IsDeleted))
                .ThenInclude(r => r.RoleOffers.Where(r => !r.IsDeleted))
                .Where(expression).AsNoTracking().ToListAsync();
        }
    }
}
