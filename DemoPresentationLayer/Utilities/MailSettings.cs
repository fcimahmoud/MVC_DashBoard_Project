using System.Net;
using System.Net.Mail;

namespace DemoPresentationLayer.Utilities
{
	public class Email
	{
		public string Subject { get; set; }
		public string Body { get; set; }
		public string Recipient { get; set; }
	}
	public class MailSettings
	{
		public static void SendEmail(Email email)
		{
			// Create SMTP Client
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;

			// Create Network Credentials 
			client.Credentials = new NetworkCredential("mahmoudcs112@gmail.com", "fmninzpafrbtzwwu");

			// Send Email
			client.Send("mahmoudcs112@gmail.com", email.Recipient, email.Subject, email.Body);
		}
	}
}
