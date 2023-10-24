using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositrios
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get ; set ; }
        private readonly CompanyDbContext DbContext;

        public UnitOfWork(CompanyDbContext dbContext)
        {
            DepartmentRepository = new DepartmentRepository(dbContext);
            EmployeeRepository = new EmployeeRepository(dbContext);
            DbContext = dbContext;
        }

        public async Task<int> Complete()
        => await DbContext.SaveChangesAsync();

        public void Dispose()
        => DbContext.Dispose();
        
    }
}
