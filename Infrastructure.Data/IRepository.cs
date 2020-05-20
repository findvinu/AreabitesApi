using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public interface IRepository<TEntity> where TEntity:class
    {
        IList<TEntity> GetEntities(bool UseCache = false);
        TEntity GetEntityById(object id);
        IQueryable<TEntity> Table { get; }
        TEntity Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        TEntity UpdateAll(TEntity entities);
        int Update(TEntity entity);
    }
}
