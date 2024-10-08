using MohammedBassem.G01.BLL.interfaces;
using MohammedBassem.G01.DAL.Data.Contexts;
using MohammedBassem.G01.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammedBassem.G01.BLL.Repositories
{
    //NOTE : The Create New = CLR
    public class DepartmentRepository :GeneraicRepository<Department>, IDepartmentRepository
    {
        //منه object فلازم اعمل Database عشان دي اللي بتتعمامل مع الAppDbContext  مع ال connection من دول لازم افتحfuncation انا لما اعمل اي 

        public DepartmentRepository(AppDbContext context) : base(context)//ASK CLR Create Object From AppDbContext  --> AppDbContext من create object انه ي CLR هنا طلبت من ال 
        {
           
        }
        // NOTE : Constractor بتنفذ ال class من ال create object لما بت
    }
}
