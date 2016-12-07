using System;
using Xamarin.Forms;
using Todo;

[assembly: Dependency(typeof(Email_iOS))]

namespace Todo
{
	public class Email_iOS : IEmail
	{
		public Email_iOS()
		{
		}
		public void sendEmail()
		{
			Device.OpenUri(new Uri("mailto:ryan.hatfield@test.com"));
		}
	}
}
