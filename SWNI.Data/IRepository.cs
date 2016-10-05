using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Data
{
    /// <summary>
    /// Repository Interface, All Repositories must implement these methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IDisposable where T : IEntity
    {
        //sync
        /// <summary>
        /// Returns total number of items
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Adds a new item to the collection
        /// </summary>
        /// <param name="item"></param>
        T Add(T item);

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="item"></param>
        T Update(T item);

        /// <summary>
        /// Removes an item from the collection
        /// </summary>
        /// <param name="item"></param>
        void Delete(T item);

        /// <summary>
        /// Checks if the repository contains this item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Contains(T item);

        /// <summary>
        /// Removes the specified item from the collection
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Remove(T item);

        /// <summary>
        /// Get the item by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Get by id and another parameter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        T GetIncluding(int id, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Returns every item in the list as queryable
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Returns every item in the list as queryable by filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns every item in the list as queryable by filter
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        //async
        /// <summary>
        /// Returns total number of items
        /// </summary>
        Task<int> CountAsync();

        /// <summary>
        /// Adds a new item to the collection asynchronously
        /// </summary>
        /// <param name="item"></param>
        Task<T> AddAsync(T item);

        /// <summary>
        /// Checks if the repository contains this item asynchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> ContainsAsync(T item);

        /// <summary>
        /// Removes the specified item from the collection asynchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(T item);

        /// <summary>
        /// Get the item by Id asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Get by id and another parameter asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<T> GetIncludingAsync(int id, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Returns every item in the list as queryable asynchronous
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<T>> GetAllAsync();

        /// <summary>
        /// Returns every item in the list as queryable by filter asynchronously
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns every item in the list as queryable by filter asynchronously
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<IQueryable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
    }
}
