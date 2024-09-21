namespace DemoPresentationLayer.ViewModels
{
	public class ResetPasswordViewModel
	{
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password & Confirm Password Doesn't Match")]
		public string ConfirmPassword { get; set; }
		public string Email { get; set; }
		public string Token { get; set; } 
	}
}
