using System.ComponentModel.DataAnnotations;

namespace MohammedBassem.G01.PL.ViewModels
{
	public class SignUpViewMoedel
	{
        [Required(ErrorMessage = "UserName Is Required !!")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "FristName Is Required !!")]
        public string FristName { get; set; }
        
        
        [Required(ErrorMessage = "LastName Is Required !!")]
        public string LastName { get; set; }
        
        
        [Required(ErrorMessage = "Email Is Required !!")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        
        
        [Required(ErrorMessage = "Password Is Required !!")]
        [DataType(DataType.Password)]
        [MinLength( 5 ,ErrorMessage = "Password Min Length is 5")]  
        public string Password { get; set; }
        
        
        
        [Required(ErrorMessage = "ConfirmPassword Is Required !!")]
        [Compare(nameof(Password) , ErrorMessage = "Confirm Password Does Not Match Password !!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        
        
        [Required(ErrorMessage = "IsAgree Is Required !!")]
        public bool IsAgree { get; set; }
    }
}
