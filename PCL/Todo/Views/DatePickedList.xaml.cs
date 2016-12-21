using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Todo
{
	// shows the list of items that matched the selected date or month
	public partial class DatePickedList : ContentPage
	{
		public DatePickedList(DatePicked dPicked)
		{
			InitializeComponent();

			List<TodoItem> datePickedList = App.Database.GetItems().ToList();			//take all the entries
			foreach (TodoItem t in datePickedList.ToList())
			{
				if (!t.TimeStamp.Year.ToString().Equals(dPicked.Year))					//match the Year, month and possibly the day
					datePickedList.Remove(t);
				else if (!t.TimeStamp.Month.ToString().Equals(dPicked.Month))
					datePickedList.Remove(t);
				else if (!t.TimeStamp.Day.ToString().Equals(dPicked.Day) && dPicked.Day != null)
					datePickedList.Remove(t);
			}

			listView.ItemsSource = datePickedList;

			#region toolbar
			ToolbarItem tbi = null;
			if (Device.OS == TargetPlatform.iOS)
			{
				tbi = new ToolbarItem("export", "export20.png", () =>
				{
					DependencyService.Get<IEmail>().sendEmail(datePickedList, dPicked);
				}, 0, 0);
			}
			//if (Device.OS == TargetPlatform.Android)
			//{ 
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

			//calculates the total mileage
			if (Device.OS == TargetPlatform.iOS)
			{
				var tbi3 = new ToolbarItem("Mileage", "mileage.png", async () =>
				{
					int totalMileage = 0;
					foreach (TodoItem t in datePickedList.ToList())
					{
						if (t.EO != null && t.SO != null)
							totalMileage += (Int32.Parse(t.EO) - Int32.Parse(t.SO));
					}
					await DisplayAlert("TotalMileage", totalMileage.ToString(), null, "Okay");
				}, 0, 0);
				ToolbarItems.Add(tbi3);
			}
			#endregion

		}

		void listItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var todoItem = (TodoItem)e.SelectedItem;
			var todoPage = new TodoItemPageX();
			todoPage.BindingContext = todoItem;

			((App)App.Current).ResumeAtTodoId = todoItem.ID;

			Navigation.PushAsync(todoPage);
		}
	}
}
