using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Services
{
    public interface IEmployeeService: IDisposable
    {
        Employee Insert(Employee employee);
        Employee Get(int employeeId);
        Employee GetByUserName(string userName);
        IEnumerable<Employee> GetAll();        
        bool Exists(Employee employee);
    }
}
