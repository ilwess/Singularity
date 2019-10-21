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
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Users ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT AllChanges ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Audios ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Videos ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Messages ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Contact ON");
            await _db.Set<TEntity>().AddAsync(entity);
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Users OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT AllChanges OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Audios OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Videos OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Messages OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Contact OFF");
        }

        public async Task Delete(int id)
        {
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Users ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT AllChanges ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Audios ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Videos ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Messages ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Contact ON");
            TEntity entity =
                await _db.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id);
            _db.Set<TEntity>().Remove(entity);
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Users OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT AllChanges OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Audios OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Videos OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Messages OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Contact OFF");

        }

        public IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>()
                .AsNoTracking();
        }

        public IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Set<TEntity>().Where(predicate);
            
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

        public async Task Update(TEntity entity)
        {
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Users ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT AllChanges ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Audios ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Videos ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Messages ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Contact ON");
            _db.Set<TEntity>().Update(entity);
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Users OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT AllChanges OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Audios OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Videos OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Messages OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Contact OFF");
        }
    }
}
