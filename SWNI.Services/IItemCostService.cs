using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Services
{
    public interface IItemCostService : IDisposable
    {
        ItemCost Insert(ItemCost ItemCost);
        bool Update(ItemCost ItemCost);
        ItemCost Get(int ItemCostId);
        IEnumerable<ItemCost> GetAll();
        IEnumerable<ItemCost> GetDefault();
        bool Exists(ItemCost ItemCost);
        bool Delete(ItemCost itemCost);
    }
}
