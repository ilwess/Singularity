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
                await _db.Set<TEntity>().AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
            _db.Set<TEntity>().Remove(entity);

        }

        public IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>()
                .AsNoTracking();
        }

        public IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Set<TEntity>().AsNoTracking().Where(predicate);
            
        }

        public async Task<TEntity> GetById(int id, params string[] includes)
        {
            DbSet<TEntity> set = _db.Set<TEntity>();
            foreach(var i in includes)
            {
                set.Include(i);
            }
            return await set
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public IQueryable<TEntity> GetByIds(string[] includes, params int[] ids)
        {
            DbSet<TEntity> set = _db.Set<TEntity>();
            foreach(var i in includes)
            {
                set.Include(i);
            }
            return set
                .AsNoTracking()
                .Where(u => ids.Contains(u.Id));
        }

        public async Task Update(TEntity entity)
        {
            _db.Attach(entity).State = EntityState.Modified;
            _db.SaveChanges();
            _db.Attach(entity).State = EntityState.Detached;
        }
    }
}
