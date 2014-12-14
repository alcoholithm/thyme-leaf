using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitPoolController
{
	private List<GameObject> unitPool;

	public new const string TAG = "[UnitPoolController]";

	//single tone
	private static UnitPoolController instance;
	
	public static UnitPoolController GetInstance()
	{
		if(instance == null) instance = new UnitPoolController();
		return instance;
	}

	public UnitPoolController()
	{
		unitPool = new List<GameObject> ();
	}

	public void AddUnit(GameObject uObj, UnitType option)
	{
		if(!isUnit(ref uObj, option))
		{
			unitPool.Add(uObj);
		}
	}

	public void RemoveUnit(GameObject uObj, UnitType option)
	{
		string str1, str2;
		str1 = "temp";
		str2 = "temp";
		switch(option)
		{
		case UnitType.AUTOMART_CHARACTER:
			str1 = uObj.GetComponent<Hero>().model.getName();
			break;
		case UnitType.AUTOMART_TOWER:
			break;
		case UnitType.TROVANT_CHARACTER:
			break;
		case UnitType.TROVANT_TOWER:
			break;
		}

		for(int i=0;i<unitPool.Count;i++)
		{
			switch(option)
			{
			case UnitType.AUTOMART_CHARACTER:
				Hero tempFunc = unitPool[i].GetComponent<Hero>();
				if(tempFunc == null) continue;
				else str2 = tempFunc.model.getName();
				break;
			case UnitType.AUTOMART_TOWER:
				break;
			case UnitType.TROVANT_CHARACTER:
				break;
			case UnitType.TROVANT_TOWER:
				break;
			}
			if(str1 == str2)
			{
				unitPool.RemoveAt(i);
				break;
			}
		}
	}

	public int CountUnit()
	{
		return unitPool.Count;
	}
	
	public GameObject ElementUnit(int idx)
	{
		if(idx < 0 || idx >= unitPool.Count) return null;

		return unitPool [idx];
	}

	public bool isUnit(ref GameObject uObj, UnitType option)
	{
		string str1, str2;
		str1 = "temp";
		str2 = "temp";
		for(int i=0;i<unitPool.Count;i++)
		{
			switch(option)
			{
			case UnitType.AUTOMART_CHARACTER:
				str1 = uObj.GetComponent<Hero>().model.getName();
				str2 = unitPool[i].GetComponent<Hero>().model.getName();
				break;
			case UnitType.AUTOMART_TOWER:
				break;
			case UnitType.TROVANT_CHARACTER:
				break;
			case UnitType.TROVANT_TOWER:
				break;
			}
			if(str1 == str2) return true;
		}
		return false;
	}

	public void DisposeUnit()
	{
		unitPool.Clear ();
		unitPool = null;
	}
}
