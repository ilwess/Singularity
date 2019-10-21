using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetById(int id, params string[] includes);
        Task Create(TEntity entity);
        Task Delete(int id);
        Task Update(TEntity entity);
    }
}
