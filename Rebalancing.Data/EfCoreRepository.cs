using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rebalancing.Data.Entities;

namespace Rebalancing.Data
{
    public interface IRepository<TEntity> where TEntity : class//, IEntity
    {
        List<TEntity> Get();
        TEntity Get(int id);
        TEntity Add(TEntity entity);
        List<TEntity> Add(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        TEntity Delete(int id);

        List<TEntity> Update(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    }

    public abstract class EfCoreRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class//, IEntity
        where TContext : DbContext
    {
        protected readonly TContext Context;

        public EfCoreRepository(TContext context)
        {
            Context = context;
        }

        public TEntity Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public List<TEntity> Add(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
            Context.SaveChanges();
            return entities.ToList();
        }

        public TEntity Delete(int id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity == null)
            {
                return entity;
            }

            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();

            return entity;
        }

        public virtual TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public virtual List<TEntity> Get()
        {
            return Context.Set<TEntity>().ToList();
        }

        public TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        public List<TEntity> Update(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().UpdateRange(entities);
            Context.SaveChanges();
            return entities.ToList();
        }
    }
}