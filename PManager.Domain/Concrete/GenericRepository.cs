using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PManager.Domain.Concrete
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal EFDbContext _context;
        internal DbSet<TEntity> _dbset;

        public GenericRepository(EFDbContext context)
        {
            this._context = context;
            this._dbset = _context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get
           (Expression<Func<TEntity, bool>> filter = null,
            Func<IEnumerable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        //
        // Similar to EntityFrameWork's DbContext.Find() Method 
        //

        public virtual TEntity Find(object id)
        {
            return _dbset.Find(id);
        }

        //
        //  Similar to EntityFrameWork's DbContext.Add() Method
        //

        public virtual void Add(TEntity entity)
        {
            _dbset.Add(entity);
        }

        //
        // Similar to EntityFrameWork's DbContext.Remove() Method
        //

        public virtual void Delete(object id)
        {
            TEntity entity = _dbset.Find(id);
            Delete(entity);
        }

        //
        // Similar to EntityFrameWork's DbContext.Remove(Entity) Method
        //

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbset.Attach(entity);
            }
            _dbset.Remove(entity);
        }

        //
        // Similar to EF's DbContext.Update() Method
        //

        public virtual void Update(TEntity entity)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
