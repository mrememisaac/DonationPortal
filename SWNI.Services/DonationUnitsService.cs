using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Services
{
    public class DonationUnitsService : IDonationUnitsService
    {
        public void Dispose()
        {
            Dispose(true);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                costServices.Dispose();
            }
        }

        private readonly IItemCostService costServices;

        public DonationUnitsService(IItemCostService chargeServices)
        {
            this.costServices = chargeServices;
        }

        public IEnumerable<DonationUnit> Get()
        {
            //int[] range = { 200, 500, 1000, 2500, 5000, 10000, 100000};
            LinkedList<int> range = new LinkedList<int>();
            int max = 150000, current = 500, count = 1;
            
            while (current < max)
            {
                current = count * 500;
                range.AddLast(current);
                count++;
            }

            string[] descriptions = { };

            List<DonationUnit> units = new List<DonationUnit>();

            var serviceCharges = costServices.GetDefault();

            foreach (var unit in range)
            {
                DonationUnit DonationUnit = new DonationUnit { Name = String.Format("{0:N}", unit), Price = unit };

                if (serviceCharges != null)
                {
                    var amountPerService = unit / serviceCharges.Count();

                    foreach (var service in serviceCharges)
                    {
                        DonationUnit.Description.Add(String.Format("{0} {1}", (int)(amountPerService / service.Amount), service.Name));
                    }
                }
                units.Add(DonationUnit);
            }
            return units;
        }

        public DonationUnit Get(string name)
        {
            return Get().FirstOrDefault(x => x.Name.Trim().Equals(name.Trim(), StringComparison.InvariantCultureIgnoreCase));
        }

        public DonationUnit Get(decimal amount)
        {
            return Get().FirstOrDefault(x => x.Price == amount);
        }
    }
}
