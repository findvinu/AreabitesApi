using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public interface IRepository<TEntity> where TEntity:class
    {
        IList<TEntity> GetEntities(bool UseCache = false);
        TEntity Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        TEntity UpdateAll(TEntity entities);
        int Update(TEntity entity);
    }
}
