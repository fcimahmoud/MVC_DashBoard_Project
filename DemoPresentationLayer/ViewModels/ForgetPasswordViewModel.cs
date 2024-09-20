namespace DemoPresentationLayer.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[EmailAddress]
		public string Email { get; set; }
	}
}
