using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMusterController
{
	public new const string TAG = "[UnitMusterController]";

	private Musters[] unitMusters;
	private MusterNames[] nameSet;
	private int currentMusterSize;

	private const int MaxMusterCount = 10;
	private const int MaxMusterUnitCount = 5;

	//single tone
	private static UnitMusterController instance;

	public static UnitMusterController GetInstance()
	{
		if(instance == null) instance = new UnitMusterController();
		return instance;
	}

	public UnitMusterController()
	{
		Initialize ();
	}

	private void Initialize()
	{
		nameSet = new MusterNames[MaxMusterCount];

		unitMusters = new Musters[MaxMusterCount];

		for(int i=0;i<MaxMusterCount;i++)
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

	public string addMuster()
	{
		if(!canMakeMuster()) return "none";

		string str = avaliableMuster ();

		unitMusters [currentMusterSize++].setName (str);

		return str;
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
				for(int k=0;k<MaxMusterUnitCount;k++)
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
				if(unitMusters[i].CountUnit() <= 1)
					freeMuster(unitMusters[i].getName());
				else
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
			obj = new GameObject[MaxMusterUnitCount]; //max count
			for(int i=0;i<MaxMusterUnitCount;i++) obj[i] = null;
			CurrentSize = 0;
		}

		public void freeMuster()
		{
			for(int i=0;i<MaxMusterUnitCount;i++)
			{
				obj[i] = null;
				CurrentSize = 0;
			}
		}

		public void addUnit(GameObject gobj)
		{
			if(CurrentSize > MaxMusterUnitCount-1) return;

			for(int i=0;i<MaxMusterUnitCount;i++)
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
			if(CurrentSize <= 1) return;

			for(int i=0;i<MaxMusterUnitCount;i++)
			{
				if(obj[i] != null)
				{
					//search nameID
					Hero tempFunc = obj[i].GetComponent<Hero>();
					if(tempFunc.model.getName() == nameID)
					{
						//change parent or remove
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
