﻿namespace DemoPresentationLayer.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "First Name Is Required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name Is Required")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "User Name Is Required")]
		public string UserName { get; set; }
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password & Confirm Password Doesn't Match")]
		public string ConfirmPassword { get; set; }
		public bool IsAgree { get; set; }
	}
}
