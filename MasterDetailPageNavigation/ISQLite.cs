using System;
using SQLite;

namespace MasterDetailPageNavigation
{
	public interface ISQLite
	{
			SQLiteConnection GetConnection();
	}
}
