using System.ComponentModel.DataAnnotations;

namespace MohammedBassem.G01.PL.ViewModels
{
	public class ResetPasswordViewModel
	{

		[Required(ErrorMessage = "Password Is Required..!!")]
		[DataType(DataType.Password)]
		[MinLength(5, ErrorMessage = "Password Min Length is 5")]
		public string Password { get; set; }



		[Required(ErrorMessage = "ConfirmPassword Is Required..!!")]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password Does Not Match Password !!")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

    }
}
