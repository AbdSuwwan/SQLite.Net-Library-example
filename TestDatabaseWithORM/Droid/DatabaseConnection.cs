using System;
using SQLite;
using  Environment=System.Environment; 

namespace TestDatabaseWithORM.Droid
{
	public class DatabaseConnection
	{

		private static SQLiteConnection _DBConnecttion = new SQLiteConnection 
			(System.IO.Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), "arraDB.db3"));


		public static SQLiteConnection getInstance() {
			return _DBConnecttion;
		}


	}
}

