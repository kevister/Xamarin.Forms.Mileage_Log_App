using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Todo
{
	public partial class MDPage : MasterDetailPage
	{
		public MDPage()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);

			masterPage.ListView.ItemSelected += OnItemSelected;

		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MasterPageItem;
			if (item != null)
			{
				Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
				masterPage.ListView.SelectedItem = null;
				IsPresented = false;
			}
		}
	}
}
