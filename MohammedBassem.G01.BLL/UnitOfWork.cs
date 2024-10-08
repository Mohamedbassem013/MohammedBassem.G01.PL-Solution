using MohammedBassem.G01.BLL.interfaces;
using MohammedBassem.G01.BLL.Repositories;
using MohammedBassem.G01.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammedBassem.G01.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IEmployeeRepository   _employeeRepository;
        private IDepartmentRepository _departmentRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _employeeRepository = new EmployeeRepository(context);
            _departmentRepository = new DepartmentRepository(context);
        }
        public IEmployeeRepository employeeRepository => _employeeRepository;
        public IDepartmentRepository departmentRepository => _departmentRepository;
    }
}
