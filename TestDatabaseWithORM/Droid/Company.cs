using System;
using SQLite;

namespace TestDatabaseWithORM.Droid
{

	public class Company : Java.Lang.Object
	{

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[MaxLength(16)]
		public string Name { get; set; }

		public override string ToString ()
		{
			return Name;
		}
	}
}

