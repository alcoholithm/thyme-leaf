using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : Manager<PathManager>  
{
	private List<GameObject> unitPool;

	public new const string TAG = "[UnitManager]";

	void Awake () 
	{
		unitPool = new List<GameObject> ();
	}

	public void AddUnit(GameObject uObj)
	{
		bool check = false;
		for(int i=0;i<unitPool.Count;i++)
		{
			if(uObj.name == unitPool[i].name)
			{
				check = true;
			}
		}
		if(!check)
		{
			unitPool.Add(uObj);
			//product?...
			//Instantiate
		}
	}

	public void RemoveUnit(GameObject uObj)
	{
		for(int i=0;i<unitPool.Count;i++)
		{
			if(uObj.name == unitPool[i].name)
			{		
				//remove?...
				//Destroy(unitPool[i])
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

	public bool isUnit(GameObject uObj)
	{
		for(int i=0;i<unitPool.Count;i++)
		{
			if(uObj.name == unitPool[i].name)
			{	
				return true;
			}
		}
		return false;
	}

	public void DisposeUnit()
	{
		unitPool.Clear ();
		unitPool = null;
	}
}
