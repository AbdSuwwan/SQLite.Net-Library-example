package testdatabasewithorm.droid;


public class Company
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_toString:()Ljava/lang/String;:GetToStringHandler\n" +
			"";
		mono.android.Runtime.register ("TestDatabaseWithORM.Droid.Company, TestDatabaseWithORM.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Company.class, __md_methods);
	}


	public Company () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Company.class)
			mono.android.TypeManager.Activate ("TestDatabaseWithORM.Droid.Company, TestDatabaseWithORM.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public java.lang.String toString ()
	{
		return n_toString ();
	}

	private native java.lang.String n_toString ();

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
