using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositrios
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly CompanyDbContext DbContext;
        public DepartmentRepository(CompanyDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        

        public IQueryable<Department> GetDepartmentsByName(string Name)
        {
            return DbContext.Departments.Where(D => D.Name.ToLower().Contains(Name.ToLower()));
        }
    }
}
