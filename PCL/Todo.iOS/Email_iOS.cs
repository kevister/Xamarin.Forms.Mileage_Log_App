﻿using System;
using Xamarin.Forms;
using Todo;
using MessageUI;
using UIKit;
using System.IO;
using Foundation;

[assembly: Dependency(typeof(Email_iOS))]

namespace Todo
{
	public class Email_iOS : IEmail
	{
		MFMailComposeViewController mailController;

		public Email_iOS()
		{
		}
		public void sendEmail(TodoItem todoItemMail)
		{

			if (MFMailComposeViewController.CanSendMail)
			{
				var comments = todoItemMail.Comments.Replace(' ', '_').Replace('\n', '_');

				var attachmentName = comments + "_" + todoItemMail.TimeStamp.ToString("yyyyMMdd") + ".csv";
				var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				var fileName = Path.Combine(documents, attachmentName);

				var header = "Date,Start Odometer,End Odometer,Comments";
				var body = header + "\n" + todoItemMail.TimeStamp.ToShortDateString() + "," + todoItemMail.SO + "," + todoItemMail.EO + "," + todoItemMail.Comments.Replace('\n', '_');

				File.WriteAllText(fileName, body);

				mailController = new MFMailComposeViewController();

				mailController.SetSubject("Mileage Data : " + attachmentName);
				mailController.SetMessageBody("this is a test", false);
				if (File.Exists(fileName))
				{
					NSData data = NSData.FromFile(fileName);
					mailController.AddAttachmentData(data, documents, attachmentName);
				}

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

