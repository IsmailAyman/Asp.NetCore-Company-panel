using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositrios
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext DbContext;
        public EmployeeRepository(CompanyDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }


        public IQueryable<Employee> GetEmployeesbyAddress(string address)
        {
            return DbContext.Employees.Where(e => e.Address == address);
        }

        public IQueryable<Employee> GetEmployeesbyName(string Name)
        {
            return DbContext.Employees.Where(e => e.Name.ToLower().Contains(Name.ToLower()));
        }
    }
}
