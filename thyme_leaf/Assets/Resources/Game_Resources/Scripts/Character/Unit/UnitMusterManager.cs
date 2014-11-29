﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMusterManager
{
	public new const string TAG = "[UnitMusterManager]";

	private Musters[] unitMusters;
	private MusterNames[] nameSet;
	private int currentMusterSize;
	private int MaxMusterCount;

	//single tone
	private static UnitMusterManager instance;

	public static UnitMusterManager GetInstance()
	{
		if(instance == null) instance = new UnitMusterManager();
		return instance;
	}

	public UnitMusterManager()
	{
		Initialize ();
	}

	private void Initialize()
	{
		MaxMusterCount = 10;

		nameSet = new MusterNames[MaxMusterCount];

		unitMusters = new Musters[MaxMusterCount];

		for(int i=0;i<10;i++)
		{
			nameSet[i].musterName = "muster"+(i+1);
			nameSet[i].use = false;

			unitMusters[i].Initialize();
		}
		currentMusterSize = 0;
	}	

	public bool canMakeMuster()
	{
		if(currentMusterSize > MaxMusterCount - 1) return false;
		return true;
	}

	public void addMuster()
	{
		if(!canMakeMuster()) return;

		string str = avaliableMuster ();

		unitMusters [currentMusterSize++].setName (str);
	}

	public void addUnits(string muster_name, string obj)
	{
		int objIdx = -1;
		for(int i=0;i<MaxMusterCount;i++)
		{
			if(unitMusters[i].getName() == obj)
			{
				objIdx = i;
				break;
			}
		}
		if(objIdx < 0) return;

		for(int i=0;i<MaxMusterCount;i++)
		{
			if(unitMusters[i].getName() == muster_name)
			{
				for(int k=0;k<5;k++)
				{
					GameObject temp = unitMusters[objIdx].getElement(k);
					if(temp != null)
					{
						unitMusters[i].addUnit(temp);
					}
				}
				unitMusters[objIdx].freeMuster();
				break;
			}
		}
	}

	public void addUnit(string muster_name, GameObject obj)
	{
		for(int i=0;i<MaxMusterCount;i++)
		{
			if(unitMusters[i].getName() == muster_name)
			{
				unitMusters[i].addUnit(obj);
			}
		}
	}

	public void removeUnit(string muster_name, string unit_nameID)
	{
		for(int i=0;i<MaxMusterCount;i++)
		{
			if(unitMusters[i].getName() == muster_name)
			{
				unitMusters[i].removeUnit(unit_nameID);
				break;
			}
		}
	}

	private string avaliableMuster()
	{
		for(int i=0;i<MaxMusterCount;i++)
		{
			if(!nameSet[i].use)
			{
				nameSet[i].use = true;
				return nameSet[i].musterName;
			}
		}
		return "error";
	}
	
	public void freeMuster(string muster_name)
	{
		for(int i=0;i<MaxMusterCount;i++)
		{
			if(nameSet[i].musterName == muster_name)
			{
				nameSet[i].use = false;
				unitMusters[i].freeMuster();
				break;
			}
		}
	}

	private struct Musters
	{
		GameObject[] obj;
		int CurrentSize;

		string name;

		public void Initialize()
		{
			obj = new GameObject[5]; //max count
			for(int i=0;i<5;i++) obj[i] = null;
			CurrentSize = 0;
		}

		public void freeMuster()
		{
			for(int i=0;i<5;i++)
			{
				obj[i] = null;
				CurrentSize = 0;
			}
		}

		public void addUnit(GameObject gobj)
		{
			if(CurrentSize > 4) return;

			for(int i=0;i<5;i++)
			{
				if(obj[i] == null)
				{
					obj[i] = gobj;
					CurrentSize++;
					break;
				}
			}
		}

		public void removeUnit(string nameID)
		{
			if(CurrentSize <= 0) return;

			for(int i=0;i<5;i++)
			{
				if(obj[i] != null)
				{
					//search nameID
					pathFinder tempFunc = obj[i].GetComponent<pathFinder>();
					if(tempFunc.getID() == nameID)
					{
						//change parent
						//...
						CurrentSize--;
						obj[i] = null;
						break;
					}
				}
			}
		}

		public int CountUnit()
		{
			return CurrentSize;
		}

		public GameObject getElement(int idx)
		{
			return obj [idx];
		}

		public void setName(string str)
		{
			name = str;
		}

		public string getName()
		{
			return name;
		}

	}

	private struct MusterNames
	{
		public string musterName;
		public bool use;
	}

}
