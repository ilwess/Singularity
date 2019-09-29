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
        IQueryable<TEntity> GetAllAsync();
        IQueryable<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetById(int id);
        Task Create(TEntity entity);
        Task Delete(int id);
        Task Update(TEntity entity);
    }
}
