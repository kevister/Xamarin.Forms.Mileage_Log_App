using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Todo
{
	public partial class MasterPage : ContentPage
	{
		public ListView ListView
		{ get { return listView; } }

		public MasterPage()
		{
			InitializeComponent();

			var masterPageItems = new List<MasterPageItem>();
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Contacts",
				//IconSource = "contacts.png",
				TargetType = typeof(TodoItemListX)
			});

			listView.ItemsSource = masterPageItems;
		}
	}
}
