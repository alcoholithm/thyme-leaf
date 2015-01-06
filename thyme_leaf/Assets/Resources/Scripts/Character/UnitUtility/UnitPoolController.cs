using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitPoolController
{
	private List<UnitObject> unitPool;
	
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
		Initialize();
	}

	public void Initialize()
	{
		if(unitPool != null)
		{
			unitPool.Clear();
			unitPool = null;
		}
		unitPool = new List<UnitObject> ();
	}
	
	public void AddUnit(UnitObject uObj)
	{
		if(!isUnit(ref uObj))
		{
			unitPool.Add(uObj);
		}
	}
	
	public void RemoveUnit(UnitObject uObj)
	{
		int id1 = -1;
	
		id1 = uObj.nameID;
		
		for(int i=0;i<unitPool.Count;i++)
		{
			if(id1 == unitPool[i].nameID)
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
	
	public UnitObject ElementUnit(int idx)
	{
		if(idx < 0 || idx >= unitPool.Count) return new UnitObject(0);
		
		return unitPool [idx];
	}
	
	public bool isUnit(ref UnitObject uObj)
	{
		int id1 = uObj.nameID;
		for(int i=0;i<unitPool.Count;i++)
		{
			if(id1 == unitPool[i].nameID) return true;
		}
		return false;
	}
	
	public void DisposeUnit()
	{
		unitPool.Clear ();
		unitPool = null;
	}
}

public struct UnitObject
{
	public GameObject obj;
	public int nameID;
	public UnitType type;
	public Tower infor_tower;
	public Hero infor_hero;
	public WChat infor_automat_center;
	public THouse infor_trovant_center;
	
	public UnitObject(GameObject obj, int id, UnitType type)
	{
		this.obj = obj;
		nameID = id;
		this.type = type;

		infor_hero = null;
		infor_tower = null;
		infor_automat_center = null;
		infor_trovant_center = null;
		switch(type)
		{
		case UnitType.AUTOMAT_CHARACTER:
		case UnitType.TROVANT_CHARACTER:
			infor_hero = obj.GetComponent<Hero>();
			break;
		case UnitType.AUTOMAT_WCHAT:
			infor_automat_center = obj.GetComponent<WChat>();
			break;
		case UnitType.TROVANT_THOUSE:
			infor_trovant_center = obj.GetComponent<THouse>();
			break;
		}
	}
	
	public UnitObject(int null_)
	{
		obj = null;
		nameID = -1;
		this.type = UnitType.AUTOMAT_CHARACTER;
		infor_hero = null;
		infor_tower = null;
		infor_automat_center = null;
		infor_trovant_center = null;
	}

	public void DataInit()
	{
		obj = null;
		nameID = -1;
		this.type = UnitType.AUTOMAT_CHARACTER;
		infor_hero = null;
		infor_tower = null;
		infor_automat_center = null;
		infor_trovant_center = null;
	}
}
