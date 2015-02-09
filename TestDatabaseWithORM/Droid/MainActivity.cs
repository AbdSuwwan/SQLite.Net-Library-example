using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using  Environment=System.Environment;
using System.Collections.Generic;



namespace TestDatabaseWithORM.Droid
{
	[Activity (Label = "TestDatabaseWithORM.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : Activity
	{
		ListView list;
		SQLiteConnection DB;
		ArrayAdapter<Company> adapter;
		List<Company> companies= new List<Company>();
		protected override void OnCreate (Bundle bundle)
		{

			base.OnCreate (bundle);
			SetContentView (Resource.Layout.main_layout);
			MoveIfDatabaseExist ();
			DB = DatabaseConnection.getInstance ();
			DB.CreateTable<Company> (CreateFlags.None);
			//AddCompany ("CATEC");
			//AddCompany ("Cubic-Art");
			//AddCompany ("Microsoft");
			//AddCompany ("Xamarin");
			//AddCompany ("Zain");
			//AddCompany ("Orange");
			list = FindViewById<ListView> (Resource.Id.listView);

			LoadData ();


			list.ItemLongClick += (sender, e) => {
				DB.Delete<Company>((companies[e.Position]).Id);
				companies.Remove(companies[e.Position]);
				//adapter.NotifyDataSetChanged();
				InitializeList ();

				//LoadData();
			};
		}


		private void LoadData(){
			ExtractToCompaniesList ();
			InitializeList ();
		}

		private void InitializeList(){
			RunOnUiThread (new Action (delegate {
				adapter = new  ArrayAdapter<Company> (this, Android.Resource.Layout.SimpleListItem1, companies);
				list.Adapter = adapter;
			}));

		}


		public  void ExtractToCompaniesList(){
			companies.Clear ();

			//query for all table records
			//var query = DB.Table<Company> ().Where(t=> t.Id>0);
			//foreach (Company company in query) {
			//	companies.Add (company);
			//}

			//using query method
			companies=DB.Query<Company>("SELECT * FROM Company WHERE Name = ?", "abd");

			//query for one record
			//Company query= DB.Get<Company>(1);
			//companies.Add (query);
			//query= DB.Get<Company>(14);
			//companies.Add (query);

		}
		public void AddCompany ( string name) {
			Company s = new Company { Name = name };
			DB.Insert (s);
			//companies.Add (s);
			//adapter.NotifyDataSetChanged ();
			LoadData();
		}
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.main_menu, menu);
			return base.OnCreateOptionsMenu (menu);
		}
		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			int id = item.ItemId;
			if (id == Resource.Id.action_add_new) {
				AddNewCompany ();
				return true;
			}
			return base.OnOptionsItemSelected (item);
		}
		private void AddNewCompany ()
		{
			AlertDialog.Builder builder = new AlertDialog.Builder (this);
			builder.SetTitle ("Add Company");

			EditText nameInput = (EditText)LayoutInflater.Inflate (Resource.Layout.add_company_dialog, null);

			builder.SetView (nameInput);
			builder.SetPositiveButton ("Create", (sender, args) => AddCompany (nameInput.Text));
			builder.SetNegativeButton ("Cancel", (IDialogInterfaceOnClickListener)null);

			AlertDialog dialog = builder.Create();
			dialog.SetCanceledOnTouchOutside (true);
			dialog.Show();


		}
		void MoveIfDatabaseExist(){
			string dbName = "arraDB.db3";
			string dbPath = System.IO.Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), dbName);
			// Check if your DB has already been extracted.
			bool exists = System.IO.File.Exists (dbPath);
			if (!exists)
			{
				using (System.IO.BinaryReader br = new System.IO.BinaryReader(Assets.Open(dbName)))
				{
					using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(new System.IO.FileStream(dbPath, System.IO.FileMode.Create)))
					{
						byte[] buffer = new byte[2048];
						int len = 0;
						while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
						{
							bw.Write (buffer, 0, len);
						}
					}
				}
			}
		}
	}
}

