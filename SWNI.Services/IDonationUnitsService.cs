using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Services
{
    public interface IDonationUnitsService
    {
        DonationUnit Get(string name);
        DonationUnit Get(decimal amount);
        IEnumerable<DonationUnit> Get();
    }
}
