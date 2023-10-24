using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="First Name is required")]
        public string FName { get; set; }
		[Required(ErrorMessage = "Last Name is required")]
		public string LName { get; set; }
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage ="invalid Email")]
		public string Email { get; set; }
		[Required(ErrorMessage ="Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage ="Confirm Password Doesn't match Password")]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }
	}
}
