using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MasterDetailPageNavigation
{
	public partial class LogItemPage : ContentPage
	{
		public LogItemPage()
		{
			InitializeComponent();
		}

		void saveClicked(object sender, EventArgs e)
		{
			var logItem = (LogItem)BindingContext;
			App.Database.SaveItem(logItem);
			this.Navigation.PopAsync();
		}

		void deleteClicked(object sender, EventArgs e)
		{
			var logItem = (LogItem)BindingContext;
			App.Database.DeleteItem(logItem.ID);
			this.Navigation.PopAsync();
		}

		void cancelClicked(object sender, EventArgs e)
		{
			var logItem = (LogItem)BindingContext;
			this.Navigation.PopAsync();
		}
	}
}
