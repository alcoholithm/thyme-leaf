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
	private int trovant_tower_ID;
	private int automart_tower_ID;

	public UnitNameGetter()
	{
		Initialize ();
	}

	public void Initialize()
	{
		trovant_ID = automart_ID = 1;
		trovant_tower_ID = automart_tower_ID = 1;
	}

	public string getNameTrovant()
	{
		string str = "trovant_" + trovant_ID;
		trovant_ID++;
		return str;
	}

	public string getNameAutomart()
	{
		string str = "automart_" + automart_ID;
		automart_ID++;
		return str;
	}

	public string getNameTrovantTower()
	{
		string str = "trovant_tower_" + trovant_tower_ID;
		trovant_tower_ID++;
		return str;
	}

	public string getNameAutomartTower()
	{
		string str = "automart_tower_" + automart_tower_ID;
		automart_tower_ID++;
		return str;
	}
}
