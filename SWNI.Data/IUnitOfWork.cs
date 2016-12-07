using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Data
{
    /// <summary>
    /// Provides access to search data
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save changes to the database
        /// </summary>
        /// <returns>0 for false, 1 for true</returns>
        int SaveChanges();

        /// <summary>
        /// Save changes to database asynchronously
        /// </summary>
        /// <returns>0 for false, 1 for true</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Attach item to database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void Attach<T>(T obj) where T : class;

        /// <summary>
        /// Add item to the collection
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="obj">Item of type T</param>
        void Add<T>(T obj) where T : class;

        /// <summary>
        /// Returns a Queryable collection of type T
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>Returns the collection of type</returns>
        IQueryable<T> Get<T>() where T : class;

        /// <summary>
        /// Removes an item of Type t from a collection of Type T l
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="item">The item</param>
        /// <returns>True if removed, false if not</returns>
        bool Remove<T>(T item) where T : class;

       
    }
}
