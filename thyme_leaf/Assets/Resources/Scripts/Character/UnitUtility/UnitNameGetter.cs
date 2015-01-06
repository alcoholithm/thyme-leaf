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
	private int trovant_center_ID;
	private int automart_center_ID;

	public UnitNameGetter()
	{
		Initialize ();
	}

	public void Initialize()
	{
		trovant_ID = 1;
		automart_ID = 1001;
		trovant_center_ID = 2001;
		automart_center_ID = 3001;
	}

	public int getNameTrovant()
	{
		int id = trovant_ID;
		trovant_ID++;
		return id;
	}

	public int getNameAutomart()
	{
		int id = automart_ID;
		automart_ID++;
		return id;
	}

	public int getNameTrovantCenter()
	{
		int id = trovant_center_ID;
		trovant_center_ID++;
		return id;
	}

	public int getNameAutomartCenter()
	{
		int id = automart_center_ID;
		automart_center_ID++;
		return id;
	}
}
