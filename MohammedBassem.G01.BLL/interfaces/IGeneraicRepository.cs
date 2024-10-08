using MohammedBassem.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammedBassem.G01.BLL.interfaces
{
    public interface IGeneraicRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(); //Department دي بتجيب كل ال
        Task<T> GetAsync(int? id); //بتاعه id من خلال ال Department دي بتجيب كل ال

        Task<int> AddAsync(T Entity);
        Task<int> UpdateAsync(T Entity);
        Task<int> DeleteAsync(T Entity);
    }
}
