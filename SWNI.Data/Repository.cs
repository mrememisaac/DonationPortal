using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SWNI.Data
{
    /// <summary>
    /// Base Implementation of IRepository used in all non special cases
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IDisposable, IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// A handle on the context
        /// </summary>
        private readonly IUnitOfWork context;

        /// <summary>
        /// Variable to hold the error message
        /// </summary>
        private string errorMessage = string.Empty;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="context">The db context</param>
        public Repository(IUnitOfWork context)
        {
            this.context = context;
        }

        #region Sync
        public int Count()
        {
            return context.Get<T>().Count();
        }

        /// <summary>
        /// Add item to the collection
        /// </summary>
        /// <param name="item">The entity</param>
        /// <returns>The inserted item</returns>
        public T Add(T item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(string.Format("{0} cannot be null", item.GetType().Name));
                }
                context.Add(item);
                context.SaveChanges();
                return item;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }

        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="item">The entity</param>
        /// <returns>The updated item</returns>
        public T Update(T item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(string.Format("{0} cannot be null", item.GetType().Name));
                }

                //Enable this line of code if you permit lazy loading
                //this.context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                this.context.SaveChanges();
                return item;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="item">The item to be deleted</param>
        public void Delete(T item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.context.Remove(item);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        /// <summary>
        /// Checks if the entity exists in the collection
        /// </summary>
        /// <param name="item">The entity to found</param>
        /// <returns>True if found else false</returns>
        public bool Contains(T item)
        {
            return context.Get<T>().FirstOrDefault(t => t == item) != null;
        }

        /// <summary>
        /// Removes the item from teh collection
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            return context.Remove(item);
        }

        public T Get(int id)
        {
            return context.Get<T>().SingleOrDefault(x => x.Id == id);
        }

        public T GetIncluding(
            int id,
            params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).SingleOrDefault(x => x.Id == id);
        }


        public IQueryable<T> GetAll()
        {
            return context.Get<T>();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return context.Get<T>().Where(predicate).AsQueryable<T>();
        }

        /// <summary>
        /// Used for Lazyloading navigation properties
        /// </summary>
        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }
            return queryable;
        }

        #endregion

        #region Async
        public async Task<int> CountAsync()
        {
            return await Task.Run(() => context.Get<T>().Count());
        }

        public async Task<T> AddAsync(T item)
        {
            context.Add(item);
            await context.SaveChangesAsync();
            return item;
        }

        public Task<bool> ContainsAsync(T item)
        {
            return Task.Run(
                () => context.Get<T>().FirstOrDefault(t => t == item) != null);
        }

        public Task<bool> RemoveAsync(T item)
        {
            return Task.Run(() => context.Remove(item));

        }

        public Task<T> GetAsync(int id)
        {
            return Task.Run(
                () => context.Get<T>().SingleOrDefault(x => x.Id == id));
        }

        public async Task<T> GetIncludingAsync(
            int id,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = await GetAllIncludingAsync(includeProperties);
            return await queryable.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<IQueryable<T>> GetAllAsync()
        {
            return Task.Run(() => context.Get<T>());
        }

        public Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate)
        {
            return Task.Run(() =>
                context.Get<T>().Where(predicate).AsQueryable<T>());
        }

        /// <summary>
        /// Used for Lazyloading navigation properties
        /// </summary>
        public Task<IQueryable<T>> GetAllIncludingAsync(
            params Expression<Func<T, object>>[] includeProperties)
        {
            return Task.Run(
                () =>
                {
                    IQueryable<T> queryable = GetAll();
                    foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                    {
                        queryable = queryable.Include(includeProperty);
                    }
                    return queryable;

                });
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //context.Dispose();
            }
        }
    }
}
