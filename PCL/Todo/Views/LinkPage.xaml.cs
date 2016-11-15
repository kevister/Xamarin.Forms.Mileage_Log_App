using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Todo
{
	public partial class LinkPage : ContentPage
	{
		public LinkPage()
		{
			InitializeComponent();

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += (s, e) =>
			{
				Device.OpenUri(new Uri(((Label)s).Text));
			};
			link.GestureRecognizers.Add(tapGestureRecognizer);
		}
	}
}
