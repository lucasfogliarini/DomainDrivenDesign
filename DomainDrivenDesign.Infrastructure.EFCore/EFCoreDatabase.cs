﻿using DomainDrivenDesign.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace DomainDrivenDesign.Infrastructure.EFCore
{
    internal class EFCoreDatabase(DatabaseContext dbContext) : IDatabase
    {
        readonly DbContext _dbContext = dbContext;

        public IQueryable<TEntity> Query<TEntity>() where TEntity : Entity
        {
            var query = _dbContext.Set<TEntity>();
            return query;
        }
        public void Add<TEntity>(TEntity entity) where TEntity : Entity
        {
            _dbContext.Add(entity);
        }
        public async Task<int> CommitAsync()
        {
            try
            {
                var changes = await _dbContext.SaveChangesAsync();
                return changes;
            }
            catch (DbUpdateException ex)
            {
                throw new ValidationException(ex.GetBaseException().Message);
            }
        }
        public void Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            _dbContext.Update(entity);
        }
        public void Remove<TEntity>(TEntity entity) where TEntity : Entity
        {
            _dbContext.Remove(entity);
        }
        public IQueryable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>>? where) where TEntity : Entity
        {
            return Query<TEntity>().Where(where);
        }
        public IQueryable<TEntity> All<TEntity>() where TEntity : Entity
        {
            return Query<TEntity>().ToList().AsQueryable();
        }
        public bool Any<TEntity>(Expression<Func<TEntity, bool>>? where = null) where TEntity : Entity
        {
            return where == null ? Query<TEntity>().Any() : Query<TEntity>().Any(where);
        }
        public TEntity? FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : Entity
        {
            return Query<TEntity>().FirstOrDefault(where);
        }
        public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }
        public async Task SeedAsync()
        {
            _dbContext.Add(new Account()
            {
                Id = 1,
                Balance = 3000,
                CreatedAt = DateTime.Now,
            });

            _dbContext.Add(new Account()
            {
                Id = 2,
                Balance = 2000,
                CreatedAt = DateTime.Now,
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}
