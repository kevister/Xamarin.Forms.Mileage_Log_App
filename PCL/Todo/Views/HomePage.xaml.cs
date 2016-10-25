using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Todo
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);
		}
		void startClicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new MDPage());
		}
	}
}
