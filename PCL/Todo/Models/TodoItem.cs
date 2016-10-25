using System;
using SQLite;

namespace Todo
{
	public class TodoItem
	{
		public TodoItem ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string SO { get; set; }
		public string EO { get; set; }
		public string Comments { get; set; }
		public DateTime TimeStamp { get; set; }

	}
}

