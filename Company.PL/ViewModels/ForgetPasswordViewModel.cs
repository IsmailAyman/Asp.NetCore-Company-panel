using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "invalid Email")]
		public string Email { get; set; }
	}
}
