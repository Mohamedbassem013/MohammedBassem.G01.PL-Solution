using AutoMapper;
using MohammedBassem.G01.DAL.Models;
using MohammedBassem.G01.PL.ViewModels;

namespace MohammedBassem.G01.PL.Mapping
{
    public class Departmentprofile : Profile
    {

        public Departmentprofile()
        {
            CreateMap<Department, DepartmentViewMpdel>().ReverseMap();
           
        }
    }
}

