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
	
	public void RemoveUnit(GameObject uObj)
	{
		int id1 = -1;
		if(uObj.layer == (int)Layer.Automart || uObj.layer == (int)Layer.Trovant)
		{
			id1 = uObj.GetComponent<Hero>().model.ID;
		}
		
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
	
	public UnitObject(GameObject obj, int id, UnitType type)
	{
		this.obj = obj;
		nameID = id;
		this.type = type;
		
		if(type != UnitType.AUTOMAT_TOWER)
		{
			infor_hero = obj.GetComponent<Hero>();
			infor_tower = null;
		}
		else
		{
			infor_hero = null;
			infor_tower = null;
		}
	}
	
	public UnitObject(int null_)
	{
		obj = null;
		nameID = -1;
		this.type = UnitType.AUTOMAT_CHARACTER;
		infor_hero = null;
		infor_tower = null;
	}
}
