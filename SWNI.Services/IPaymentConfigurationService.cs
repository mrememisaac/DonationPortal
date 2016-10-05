using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Services
{
    public interface IPaymentConfigurationService : IDisposable
    {
        IEnumerable<PaymentConfiguration> GetAll();
        PaymentConfiguration Insert(PaymentConfiguration config);
        bool Update(PaymentConfiguration config);
        void Delete(PaymentConfiguration config);
        PaymentConfiguration Get(int id);       
        PaymentConfiguration GetDefault();
    }
}
