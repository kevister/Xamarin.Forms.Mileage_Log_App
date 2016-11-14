using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Todo
{
	class DateViewModel : INotifyPropertyChanged
	{
		DateTime dateTime;

		public event PropertyChangedEventHandler PropertyChanged;

		public DateViewModel()
		{
			this.DateTime = DateTime.Now;

			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
				{
					this.DateTime = DateTime.Now;
					return true;
				});
		}

		public DateTime DateTime
		{
			set
			{
				if (dateTime != value)
				{
					dateTime = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this,
							new PropertyChangedEventArgs("DateTime"));
					}
				}
			}
			get
			{
				return dateTime;
			}
		}
	}
}
