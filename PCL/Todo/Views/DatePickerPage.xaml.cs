using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Todo
{
	public partial class DatePickerPage : ContentPage
	{
		public DatePickerPage(String command)
		{
			InitializeComponent();
			// hide the Day section if month is selected
			if (command == "Month")
			{
				PickDay.IsVisible = false;
				PickDay.IsEnabled = false;
			}
			// else, show the Day section
			else
			{
				PickDay.IsVisible = true;
				PickDay.IsEnabled = true;
			}

		}

		//when done is clicked, the date entered will be save and pushed to DatePickedList
		void doneClicked(object sender, EventArgs e)
		{
			var datePicked = (DatePicked)BindingContext;
			var datePickedList = new DatePickedList(datePicked);

			Navigation.PushAsync(datePickedList);
		}
	}
}
