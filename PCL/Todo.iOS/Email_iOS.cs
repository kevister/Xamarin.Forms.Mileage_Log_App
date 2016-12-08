using System;
using Xamarin.Forms;
using Todo;
using MessageUI;
using UIKit;

[assembly: Dependency(typeof(Email_iOS))]

namespace Todo
{
	public class Email_iOS : IEmail
	{
		MFMailComposeViewController mailController;

		public Email_iOS()
		{
		}
		public void sendEmail()
		{
			Device.OpenUri(new Uri("mailto:ryan.hatfield@test.com"));

			if (MFMailComposeViewController.CanSendMail)
			{
				mailController = new MFMailComposeViewController();

				// do mail operations here
				//mailController.SetToRecipients(new string[] { "john@doe.com" });
				mailController.SetSubject("mail test");
				mailController.SetMessageBody("this is a test", false);

				mailController.Finished += (object s, MFComposeResultEventArgs args) =>
				{
					Console.WriteLine(args.Result.ToString());
					args.Controller.DismissViewController(true, null);
				};

				UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(mailController, true, null);
			}
		}
	}
}
