using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TekoEmployeesMvc.Models;

public class GenericRepository<TEntity> where TEntity : class
{
    // TODO: 
    // 1) Isolate dbSet using lock 

    private List<TEntity> dbSet;

    public GenericRepository()
    {
        this.dbSet = new List<TEntity>(); 
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
    {
        IQueryable<TEntity> query = dbSet.AsQueryable();
        if (filter != null)
            query = query.Where(filter);
        if (orderBy != null)
            return orderBy(query).ToList();
        return query.ToList();
    }

    public virtual void Insert(TEntity entity)
    {
        if (entity == null) throw new System.Exception("Entity could not be null"); 

        dbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
    }

    public virtual void Delete(TEntity entityToDelete)
    {
    }

    public virtual void Update(TEntity entityToUpdate)
    {
    }
}