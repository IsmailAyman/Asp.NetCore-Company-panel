using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {

        public IQueryable<Employee> GetEmployeesbyAddress(string address);

        public IQueryable<Employee> GetEmployeesbyName(string Name);

    }
}
