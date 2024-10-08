using AutoMapper;
using MohammedBassem.G01.DAL.Models;
using MohammedBassem.G01.PL.ViewModels;

namespace MohammedBassem.G01.PL.Mapping.Employees
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
