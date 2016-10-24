using System;
using SQLite;

namespace MasterDetailPageNavigation
{
	public class LogItem
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string SO { get; set; }
		public string EO { get; set; }

	}
}

