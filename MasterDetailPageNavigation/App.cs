using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace MasterDetailPageNavigation
{
	public class App : Application
	{
		static LogItemDatabase database;

		public App ()
		{
			MainPage = new MasterDetailPageNavigation.MainPage ();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
			Debug.WriteLine("OnStart");

			// always re-set when the app starts
			// users expect this (usually)
			//			Properties ["ResumeAtTodoId"] = "";
			//if (Properties.ContainsKey("ResumeAtTodoId"))
			//{
			//	var rati = Properties["ResumeAtTodoId"].ToString();
			//	Debug.WriteLine("   rati=" + rati);
			//	if (!String.IsNullOrEmpty(rati))
			//	{
			//		Debug.WriteLine("   rati = " + rati);
			//		ResumeAtTodoId = int.Parse(rati);

			//		if (ResumeAtTodoId >= 0)
			//		{
			//			var todoPage = new TodoItemPageX();
			//			todoPage.BindingContext = Database.GetItem(ResumeAtTodoId);

			//			MainPage.Navigation.PushAsync(
			//				todoPage,
			//				false); // no animation
			//		}
			//	}
			//}
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

		public static LogItemDatabase Database
		{
			get
			{
				if (database == null)
				{
					database = new LogItemDatabase();
				}
				return database;
			}
		}
	}
}

