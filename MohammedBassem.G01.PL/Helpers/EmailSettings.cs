//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MohammedBassem.G01.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MohammedBassem.G01.PL.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail(Emails email)
		{
			// Mail Server : gmail.com

			// Smtp
			// Note : خطوات ثابته
			var client = new SmtpClient("smtp.gmail.com", 587);
		
			client.EnableSsl = true; // عشان يشفر البورت ده ال 587 بس مش هيتشفر لاني مش معايا شهاده


			client.Credentials = new NetworkCredential("mohammedatta095@gmail.com", "ecnqvfkiqraxoorz");


			client.Send("mohammedatta095@gmail.com", email.To, email.Subject , email.Body);
		
		}   
	}
}
