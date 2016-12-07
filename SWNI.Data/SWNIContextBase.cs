using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Data
{
    /// <summary>
    /// SWNI Portal data context base, methods the context must implement
    /// </summary>
    public abstract class SWNIContextBase : DbContext, IUnitOfWork
    {
        public SWNIContextBase(String nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = true; //set to false to dodge serialization issues
            this.Configuration.ProxyCreationEnabled = false;
        }

        public IQueryable<T> Get<T>() where T : class
        {
            return Set<T>();
        }

        public bool Remove<T>(T item) where T : class
        {
            try
            {
                Set<T>().Remove(item);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        public new Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public void Attach<T>(T obj) where T : class
        {
            Set<T>().Attach(obj);
        }

        public void Add<T>(T obj) where T : class
        {
            Set<T>().Add(obj);
        }

        
    }
}
