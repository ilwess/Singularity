using Domain.EFContext;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly SingularityContext _db;

        public Repository(SingularityContext context)
        {
            _db = context;
        }
        public async Task Create(TEntity entity)
        {
            await _db.Set<TEntity>().AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            TEntity entity =
                await _db.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id);
            _db.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> GetAllAsync()
        {
            return _db.Set<TEntity>()
                .AsNoTracking();
        }

        public IQueryable<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Set<TEntity>().Where(predicate);
            
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _db.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Update(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
        }
    }
}
