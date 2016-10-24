using Xamarin.Forms;

namespace MasterDetailPageNavigation
{
	public partial class LogPage : ContentPage
	{
		public LogPage ()
		{
			InitializeComponent ();

			#region toolbar
			ToolbarItem tbi = null;
			if (Device.OS == TargetPlatform.iOS)
			{
				tbi = new ToolbarItem("+", null, () =>
				{
					//var todoItem = new TodoItem();
					var todoPage = new ReminderPage();
					//todoPage.BindingContext = todoItem;
					Navigation.PushAsync(todoPage);
				}, 0, 0);
			}

			ToolbarItems.Add(tbi);

			#endregion toolbar
		}

		void listItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var logItem = (LogItem)e.SelectedItem;
			var logPage = new ReminderPage();
			//todoPage.BindingContext = todoItem;

			//((App)App.Current).ResumeAtTodoId = todoItem.ID;
			//Debug.WriteLine("setting ResumeAtTodoId = " + todoItem.ID);

			Navigation.PushAsync(logPage);
		}
	}
}

