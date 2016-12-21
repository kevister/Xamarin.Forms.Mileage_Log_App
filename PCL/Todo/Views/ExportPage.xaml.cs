using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Todo
{
	public partial class ExportPage : ContentPage
	{
		public ExportPage()
		{
			InitializeComponent();

		}

		//So far, when the export button is clicked, just export every entry
		void exportClicked(object sender, EventArgs e)
		{
			var dp = new DatePicked();
			dp.Day = "";
			dp.Year = "Everything";
			dp.Month = "";
			List<TodoItem> datePickedList = App.Database.GetItems().ToList();
			DependencyService.Get<IEmail>().sendEmail(datePickedList, dp);
		}
	}
}
