using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MasterDetailPageNavigation
{
	public class LogItemDatabase
	{
		static object locker = new object();

		SQLiteConnection database;
		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public LogItemDatabase()
		{
			database = DependencyService.Get<ISQLite>().GetConnection();
			// create the tables
			database.CreateTable<LogItem>();
		}

		public IEnumerable<LogItem> GetItems()
		{
			lock (locker)
			{
				return (from i in database.Table<LogItem>() select i).ToList();
			}
		}

		public LogItem GetItem(int id)
		{
			lock (locker)
			{
				return database.Table<LogItem>().FirstOrDefault(x => x.ID == id);
			}
		}

		public int SaveItem(LogItem item)
		{
			lock (locker)
			{
				if (item.ID != 0)
				{
					database.Update(item);
					return item.ID;
				}
				else {
					return database.Insert(item);
				}
			}
		}

		public int DeleteItem(int id)
		{
			lock (locker)
			{
				return database.Delete<LogItem>(id);
			}
		}
	}
}