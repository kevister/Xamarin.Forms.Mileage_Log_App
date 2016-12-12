using System;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Todo
{
	public class App : Application
	{
		static TodoItemDatabase database;

		public App ()
		{
			Resources = new ResourceDictionary ();
			Resources.Add ("primaryGreen", Color.FromHex("91CA47"));
			Resources.Add ("primaryDarkGreen", Color.FromHex ("6FA22E"));

			//nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
			//nav.BarTextColor = Color.Black;
			List<TodoItem> datePickedList = App.Database.GetItems().ToList();
			foreach (TodoItem t in datePickedList.ToList())
			{
				if (t.Comments == null && t.EO == null && t.Label == null && t.SO == null)
					App.Database.DeleteItem(t.ID);
			}

			var nav = new NavigationPage(new MDPage());
			MainPage = nav;
		}

		public static TodoItemDatabase Database {
			get { 
				if (database == null) {
					database = new TodoItemDatabase ();
				}
				return database; 
			}
		}

		public int ResumeAtTodoId { get; set; }

		protected override void OnStart()
		{
			Debug.WriteLine ("OnStart");

			// always re-set when the app starts
			// users expect this (usually)
			//			Properties ["ResumeAtTodoId"] = "";
			if (Properties.ContainsKey("ResumeAtTodoId"))
			{
				var rati = Properties["ResumeAtTodoId"].ToString();
				Debug.WriteLine("   rati=" + rati);
				if (!String.IsNullOrEmpty(rati))
				{
					Debug.WriteLine("   rati = " + rati);
					ResumeAtTodoId = int.Parse(rati);

					if (ResumeAtTodoId >= 0)
					{
						var todoPage = new TodoItemPageX();
						todoPage.BindingContext = Database.GetItem(ResumeAtTodoId);

						MainPage.Navigation.PushAsync(
							new MDPage(), false); // no animation
					}
				}
			}
			else {
				MainPage.Navigation.PushAsync(new MDPage(), false);
			}
		}
		protected override void OnSleep()
		{
			Debug.WriteLine ("OnSleep saving ResumeAtTodoId = " + ResumeAtTodoId);
			// the app should keep updating this value, to
			// keep the "state" in case of a sleep/resume
			Properties ["ResumeAtTodoId"] = ResumeAtTodoId;
		}
		protected override void OnResume()
		{
			Debug.WriteLine ("OnResume");
//			if (Properties.ContainsKey ("ResumeAtTodoId")) {
//				var rati = Properties ["ResumeAtTodoId"].ToString();
//				Debug.WriteLine ("   rati="+rati);
//				if (!String.IsNullOrEmpty (rati)) {
//					Debug.WriteLine ("   rati = " + rati);
//					ResumeAtTodoId = int.Parse (rati);
//
//					if (ResumeAtTodoId >= 0) {
//						var todoPage = new TodoItemPage ();
//						todoPage.BindingContext = Database.GetItem (ResumeAtTodoId);
//
//						MainPage.Navigation.PushAsync (
//							todoPage,
//							false); // no animation
//					}
//				}
//			}
		}
	}
}

