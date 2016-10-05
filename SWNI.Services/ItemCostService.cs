using SWNI.Data;
using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Services
{
    public class ItemCostService : IItemCostService
    {
        public void Dispose()
        {
            this.Dispose(true);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.repository.Dispose();
            }
        }
        private readonly IRepository<ItemCost> repository;

        public ItemCostService(IRepository<ItemCost> repository)
        {
            this.repository = repository;
        }

        public ItemCost Insert(ItemCost ItemCost)
        {
            return repository.Add(ItemCost);
        }

        public bool Update(ItemCost ItemCost)
        {
            ItemCost.DateUpdated = DateTime.Now;
            repository.Update(ItemCost);
            return true;
        }

        public ItemCost Get(int ItemCostId)
        {
            return repository.Get(ItemCostId);
        }

        public IEnumerable<ItemCost> GetAll()
        {
            return repository.GetAll();
        }

        public bool Exists(ItemCost itemCost)
        {
            return repository.GetAll(x => x.Name.Trim().Equals(itemCost.Name.Trim(), StringComparison.InvariantCultureIgnoreCase) && x.Amount == itemCost.Amount).Any();
        }

        public IEnumerable<ItemCost> GetDefault()
        {
            return null;
        }

        public bool Delete(ItemCost itemCost)
        {
            return repository.Remove(itemCost);
        }
    }
}
