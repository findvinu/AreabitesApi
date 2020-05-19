using Microsoft.EntityFrameworkCore;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity:class
    {
        private readonly DataContext _context;
        private DbSet<TEntity> _entities;
        //private RedisClient redisClient { set; get; }
       // private IRedisTypedClient<TEntity> _redisClient;
        public Repository(DataContext context)
        {
            _context = context;
           // this._redisClient = _redisClient;
        }
        public IList<TEntity> GetEntities(bool UseCache = false)
        {

            List<TEntity> Entities;
            //if (UseCache)
            //{
            //    //get content from Cache
            //    string key = typeof(TEntity).Name;

            //    Entities = redisClient.Get<List<TEntity>>(key);
            //    if (Entities == null || Entities.Count.Equals(0))
            //    {
            //        Entities = this.Entities.ToList();
            //        redisClient.Set(key, Entities, TimeSpan.FromHours(24));

            //    }

            //    return Entities;
            //}
            Entities = this.Entities.ToList();

            return Entities;
        }

        public TEntity GetEntityById(object id)
        {
            return this.Entities.Find(id);
        }
        public IQueryable<TEntity> Table => this.Entities;
        public TEntity Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            this.Entities.Add(entity);
            this._context.SaveChanges();
            return entity;
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            this.Entities.AddRange(entities);
            this._context.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            this.Entities.Remove(entity);
            this._context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            this.Entities.RemoveRange(entities);
            this._context.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            return this._context.SaveChanges();
        }

        public TEntity UpdateAll(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            this._context.Entry(entity).State = EntityState.Modified;
            this._context.SaveChanges();
            return entity;
        }

        private DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<TEntity>();
                }
                return _entities as DbSet<TEntity>;
            }
        }
    }
}

