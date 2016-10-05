using SWNI.Data;
using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Services
{
    public class PaymentConfigurationService : IPaymentConfigurationService
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
        private readonly IRepository<PaymentConfiguration> repository;

        public PaymentConfigurationService(IRepository<PaymentConfiguration> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<PaymentConfiguration> GetAll()
        {
            return repository.GetAll();
        }

        public PaymentConfiguration Insert(PaymentConfiguration config)
        {
            return repository.Add(config);
        }

        public bool Update(PaymentConfiguration config)
        {
            repository.Update(config);
            return true;
        }

        public void Delete(PaymentConfiguration config)
        {
            repository.Delete(config);
        }

        public PaymentConfiguration Get(int id)
        {
            return repository.Get(id);
        }

        public PaymentConfiguration GetDefault()
        {
            return repository.GetAll().FirstOrDefault();
        }
    }
}
