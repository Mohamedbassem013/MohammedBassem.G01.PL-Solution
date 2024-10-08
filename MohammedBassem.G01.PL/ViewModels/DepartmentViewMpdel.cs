using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MohammedBassem.G01.PL.ViewModels 
{
    public class DepartmentViewMpdel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "The Code is required")]
        public string Code { get; set; }


        [Required(ErrorMessage = "The Date is required")]
        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }


    }
}
