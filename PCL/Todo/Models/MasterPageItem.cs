using System;

using Xamarin.Forms;

namespace Todo
{
	// needed for the detail pages on the Master-detail page
	public class MasterPageItem
	{

		public string Title { get; set; }

		public string IconSource { get; set; }

		public Type TargetType { get; set; }

	}
}

