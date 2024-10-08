using MohammedBassem.G01.BLL.interfaces;
using MohammedBassem.G01.DAL.Data.Contexts;
using MohammedBassem.G01.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammedBassem.G01.BLL.Repositories
{
    public class EmployeeRepository :GeneraicRepository<Employee> , IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) :base(context) 
        {       
        }

        public async Task<IEnumerable<Employee>> GetByNameAsync(string name)
        {
            return await _context.employees.Where(E => E.Name.ToLower().Contains(name.ToLower())).Include(E => E.WorkFor).ToListAsync();
        }
    }
}