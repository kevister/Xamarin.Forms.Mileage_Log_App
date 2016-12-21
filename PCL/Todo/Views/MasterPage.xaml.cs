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

			//the detail pages
			var masterPageItems = new List<MasterPageItem>();
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Log",
				IconSource = "log.png",
				TargetType = typeof(TodoItemListX)
			});

			masterPageItems.Add(new MasterPageItem
			{
				Title = "Export",
				IconSource = "export.png",
				TargetType = typeof(ExportPage)
			});

			masterPageItems.Add(new MasterPageItem
			{
				Title = "Links",
				IconSource = "link.png",
				TargetType = typeof(LinkPage)
			});

			listView.ItemsSource = masterPageItems;
		}
	}
}
