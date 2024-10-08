using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MohammedBassem.G01.PL.ViewModels
{
    public class RoleViewModel
    {
        public string? Id { get; set; }
        public string RoleName { get; set; }
    }
}
