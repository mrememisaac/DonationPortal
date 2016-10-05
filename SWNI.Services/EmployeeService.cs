using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWNI.Entities;
using SWNI.Data;

namespace SWNI.Services
{
    public class EmployeeService : IEmployeeService
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

        private readonly IRepository<Employee> repository;

        public EmployeeService(IRepository<Employee> repository)
        {
            this.repository = repository;
        }

        public Employee Insert(Employee employee)
        {
            if (Exists(employee))
            {
                throw new Exception("We cannot have two employees with the same username");
            }
            return Task.Run(() => repository.Add(employee)).Result;
        }

        public Employee Get(int employeeId)
        {
            return Task.Run(() => repository.Get(employeeId)).Result;
        }

        public Employee GetByUserName(string userName)
        {
            return Task.Run(() => repository.GetAll(x => x.UserName.Trim().ToLower().Equals(userName.Trim().ToLower())).FirstOrDefault()).Result;
        }


        public IEnumerable<Employee> GetAll()
        {
            return Task.Run(() => repository.GetAll()).Result;
        }

       

        public bool Exists(Employee employee)
        {
            return Task.Run(() => repository.GetAll(x => x.UserName.Trim().Equals(employee.UserName.Trim(), StringComparison.InvariantCultureIgnoreCase)).Any()).Result;
        }
    }
}
