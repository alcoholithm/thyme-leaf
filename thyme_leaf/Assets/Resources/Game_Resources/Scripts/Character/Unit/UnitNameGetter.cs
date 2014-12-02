using UnityEngine;
using System.Collections;

public class UnitNameGetter 
{
	public new const string TAG = "[UnitNameGetter]";

	//single tone
	private static UnitNameGetter instance;
	
	public static UnitNameGetter GetInstance()
	{
		if(instance == null) instance = new UnitNameGetter();
		return instance;
	}

	private int trovant_ID;
	private int automart_ID;
	private string automart_name;
	private string trovant_name;

	public UnitNameGetter()
	{
		Initialize ();
	}

	private void Initialize()
	{
		trovant_ID = automart_ID = 1;
		automart_name = "automart_";
		trovant_name = "trovant_";
	}

	public string getNameTrovant()
	{
		string str = trovant_name + trovant_ID;
		trovant_ID++;
		return str;
	}

	public string getNameAutomart()
	{
		string str = automart_name + automart_ID;
		automart_ID++;
		return str;
	}
}
