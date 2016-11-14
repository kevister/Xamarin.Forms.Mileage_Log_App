using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Todo
{
	public partial class DatePickedList : ContentPage
	{
		public DatePickedList(DatePicked dPicked)
		{
			InitializeComponent();

			#region toolbar
			ToolbarItem tbi = null;
			if (Device.OS == TargetPlatform.iOS)
			{
				tbi = new ToolbarItem("export", "export20.png", () =>
				{
					var todoItem = new TodoItem();
					todoItem.TimeStamp = DateTime.Now;
					var todoPage = new TodoItemPageX();
					todoPage.BindingContext = todoItem;
					Navigation.PushAsync(todoPage);
				}, 0, 0);
			}
			//if (Device.OS == TargetPlatform.Android)
			//{ // BUG: Android doesn't support the icon being null
			//	tbi = new ToolbarItem("+", "plus", () =>
			//	{
			//		var todoItem = new TodoItem();
			//		var todoPage = new TodoItemPageX();
			//		todoPage.BindingContext = todoItem;
			//		Navigation.PushAsync(todoPage);
			//	}, 0, 0);
			//}
			//if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
			//{
			//	tbi = new ToolbarItem("Add", "add.png", () =>
			//	{
			//		var todoItem = new TodoItem();
			//		var todoPage = new TodoItemPageX();
			//		todoPage.BindingContext = todoItem;
			//		Navigation.PushAsync(todoPage);
			//	}, 0, 0);
			//}

			ToolbarItems.Add(tbi);

			if (Device.OS == TargetPlatform.iOS)
			{
				var tbi2 = new ToolbarItem("Calendar", "calendar.png", async () =>
				{
					var action = await DisplayActionSheet("Search by ...", "Cancel", null, "Month", "Day");
					var datePicked = new DatePicked();
					var datepickerPage = new DatePickerPage(action);
					datepickerPage.BindingContext = datePicked;
					if (Navigation != null)
						await Navigation.PushAsync(datepickerPage);
				}, 0, 0);
				ToolbarItems.Add(tbi2);
			}
			#endregion

			List<TodoItem> datePickedList = App.Database.GetItems().ToList();
			foreach (TodoItem t in datePickedList.ToList())
			{
				if (!t.TimeStamp.Year.ToString().Equals(dPicked.Year))
					datePickedList.Remove(t);
				else if (!t.TimeStamp.Month.ToString().Equals(dPicked.Month))
					datePickedList.Remove(t);
				else if (!t.TimeStamp.Day.ToString().Equals(dPicked.Day))
					datePickedList.Remove(t);
			}

			listView.ItemsSource = datePickedList;
		}

		void listItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var todoItem = (TodoItem)e.SelectedItem;
			var todoPage = new TodoItemPageX();
			todoPage.BindingContext = todoItem;

			((App)App.Current).ResumeAtTodoId = todoItem.ID;
			//Debug.WriteLine("setting ResumeAtTodoId = " + todoItem.ID);

			Navigation.PushAsync(todoPage);
		}
		//protected override void OnAppearing()
		//{
		//	base.OnAppearing();
		//	// reset the 'resume' id, since we just want to re-start here
		//	((App)App.Current).ResumeAtTodoId = -1;
		//	listView.ItemsSource = App.Database.GetItems();
		//}
	}
}
