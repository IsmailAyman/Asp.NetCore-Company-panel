using Company.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.PL.Helpers
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);
			Client.EnableSsl= true;
			Client.Credentials = new NetworkCredential("ismailayman102@gmail.com", "krocorzhsugaeole");
			Client.Send("ismailayman102@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
