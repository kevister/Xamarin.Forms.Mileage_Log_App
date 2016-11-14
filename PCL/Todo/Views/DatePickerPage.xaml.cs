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

			if (command == "Month")
			{
				PickDay.IsVisible = false;
				PickDay.IsEnabled = false;
			}
			else
			{
				PickDay.IsVisible = true;
				PickDay.IsEnabled = true;
			}

		}

		void doneClicked(object sender, EventArgs e)
		{
			var datePicked = (DatePicked)BindingContext;
			var datePickedList = new DatePickedList(datePicked);

			Navigation.PushAsync(datePickedList);
		}
	}
}
