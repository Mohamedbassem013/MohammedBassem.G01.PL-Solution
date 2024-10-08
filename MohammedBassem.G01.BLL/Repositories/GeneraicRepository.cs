using MohammedBassem.G01.BLL.interfaces;
using MohammedBassem.G01.DAL.Data.Contexts;/////
using MohammedBassem.G01.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammedBassem.G01.BLL.Repositories
{
    public class GeneraicRepository<T> : IGeneraicRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbContext _context;

        public GeneraicRepository(AppDbContext context)
        {
            _context = context;
        }
        
        // Async Return : (Void , Task , Task<> )
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.employees.Include(E => E.WorkFor).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.Set<T>().AsNoTracking().ToListAsync();
            } 
        }
        public async Task<T> GetAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<int> AddAsync(T Entity)
        {
            await _context.Set<T>().AddAsync(Entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(T Entity)
        {
            _context.Set<T>().Update(Entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(T Entity)
        {
            _context.Set<T>().Remove(Entity);
            return await _context.SaveChangesAsync();
        }
    }
}
