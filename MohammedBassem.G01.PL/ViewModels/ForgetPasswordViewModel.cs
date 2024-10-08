using System.ComponentModel.DataAnnotations;

namespace MohammedBassem.G01.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required..!!")]
        [EmailAddress(ErrorMessage = "Invalid Email..!!")]
        public string Email { get; set; }

    }
}
