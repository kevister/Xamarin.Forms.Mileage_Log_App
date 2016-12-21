using System;
using System.Collections.Generic;
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

		//single item export
		public void sendEmail(TodoItem todoItemMail)
		{

			if (MFMailComposeViewController.CanSendMail)
			{

				var comments = "";

				//setting up the name of the document
				if (todoItemMail.Comments != null)
					comments = todoItemMail.Comments.Replace(' ', '_').Replace('\n', '_');

				var attachmentName = comments + "_" + todoItemMail.TimeStamp.ToString("yyyyMMdd") + ".csv";
				var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);			//path of the document
				var fileName = Path.Combine(documents, attachmentName);

				//first row of the document
				var header = "Date,Start Odometer,End Odometer,Comments";
				var so = "";
				var eo = "";
				var itemComments = "";

				//if any field is empty
				if (todoItemMail.SO == null)
					so = "0";
				else
					so = todoItemMail.SO;
				if (todoItemMail.EO == null)
					eo = "0";
				else
					eo = todoItemMail.EO;
				if (todoItemMail.Comments == null)
					itemComments = "";
				else
					itemComments = todoItemMail.Comments.Replace('\n', '_');

				// enter the fields into the document
				var body = header + "\n" + todoItemMail.TimeStamp.ToShortDateString() + "," + so + "," + eo + "," + itemComments;

				//the actual write everything to the file step
				File.WriteAllText(fileName, body);

				//mail portion
				mailController = new MFMailComposeViewController();

				mailController.SetSubject("Mileage Data : " + attachmentName);	// subject field
				//mailController.SetMessageBody("this is a test", false);		<--- if you want anything in the message body

				//add attachment
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

		//send a list of item list
		public void sendEmail(List<TodoItem> list, DatePicked dp)
		{
			var body = "Date,Start Odometer,End Odometer,Comments";

			var attachmentName = dp.Year + "_" + dp.Month + "_" + dp.Day + ".csv";				//put the YYYYMMDD as the title of the mail
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var fileName = Path.Combine(documents, attachmentName);

			var so = "";
			var eo = "";
			var itemComments = "";

			//loop through each item to add them to the body
			foreach (TodoItem ti in list)
			{
				if (MFMailComposeViewController.CanSendMail)
				{
					//turn null items into empty values, so app won't crash
					if (ti.SO == null)
						so = "0";
					else
						so = ti.SO;
					if (ti.EO == null)
						eo = "0";
					else
						eo = ti.EO;
					if (ti.Comments == null)
						itemComments = "";
					else
						itemComments = ti.Comments.Replace('\n', '_');

					body = body + "\n" + ti.TimeStamp.ToShortDateString() + "," + so + "," + eo + "," + itemComments;
				}
			}

			File.WriteAllText(fileName, body);

			if (MFMailComposeViewController.CanSendMail)
			{
				mailController = new MFMailComposeViewController();

				mailController.SetSubject("Mileage Data : " + attachmentName);
				//mailController.SetMessageBody("this is a test", false);
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

