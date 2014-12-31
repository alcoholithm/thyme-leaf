﻿using UnityEngine;
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
			nameSet[i].musterName = "muster_"+(i+1);
			nameSet[i].use = false;
			
			unitMusters[i].Initialize();
		}
		currentMusterSize = 0;
	}	

	//command...
	public void CommandAttack(string muster_name, string current_unit_name)
	{
		int idx = -1;
		for(int i=0;i<MaxMusterCount;i++)
		{
			if(unitMusters[i].getName() == muster_name)
			{
				idx = i;
				break;
			}
		}
		if(idx < 0) return;

		int unit_idx = -1;
		for(int i=0;i<MaxMusterUnitCount;i++)
		{
			Hero hero = unitMusters[idx].getElement(i);
			if(hero != null && hero.model.Name == current_unit_name)
			{
				unit_idx = i;
				break;
			}
		}
		if(unit_idx < 0) return;
		Hero leader = unitMusters [idx].getElement (unit_idx);

		//lock on system setting...
		Debug.Log ("command attack");
		for(int i=0;i<MaxMusterUnitCount;i++)
		{
			Hero hero = unitMusters[idx].getElement(i);
			if(hero != null && hero.model.Name != current_unit_name)
			{
				if(hero.target == null && hero.StateMachine.CurrentState == HeroState_Moving.Instance)
				{
					Debug.Log("i : " + i);
					hero.target = leader.helper.attack_target;
				}
			}
		}
	}

	public void CommandMove(string muster_name, string current_unit_name)
	{
		int idx = -1;
		for(int i=0;i<MaxMusterCount;i++)
		{
			if(unitMusters[i].getName() == muster_name)
			{
				idx = i;
				break;
			}
		}
		if(idx < 0) return;
		
		int unit_idx = -1;
		for(int i=0;i<MaxMusterUnitCount;i++)
		{
			Hero hero = unitMusters[idx].getElement(i);
			if(hero != null && hero.model.Name == current_unit_name)
			{
				unit_idx = i;
				break;
			}
		}
		if(unit_idx < 0) return;
		Hero leader = unitMusters [idx].getElement (unit_idx);
		Debug.Log ("leader : "+leader.model.Name);

		Vector3 d = (leader.helper.gesture_endpoint - leader.helper.gesture_startpoint);
		d.Normalize ();

		for(int i=0;i<MaxMusterUnitCount;i++)
		{
			Hero hero = unitMusters[idx].getElement(i);
			if(hero != null && hero.model.Name != current_unit_name &&
			   hero.StateMachine.CurrentState == HeroState_Moving.Instance)
			{
//				Debug.Log("ohter : "+hero.model.Name);
				hero.helper.gesture_startpoint = hero.helper.getPos();
				hero.helper.gesture_endpoint = hero.helper.gesture_startpoint + (d * 100);
				if(hero.helper.SelectPathNode(hero.helper.gesture_startpoint, hero.helper.gesture_endpoint, hero.getLayer(), FindingNodeDefaultOption.MUSTER_COMMAND))
				{
//					Debug.Log("move okay");
					hero.controller.setMoveTrigger(true);
				}
			}
		}
	}
	
	public bool canMakeMuster()
	{
		if(currentMusterSize > MaxMusterCount - 1) return false;
		return true;
	}
	
	public Hero LeaderObj(string muster_name)
	{
		int idx = -1;
		for(int i=0;i<MaxMusterCount;i++)
		{
			if(unitMusters[i].getName() == muster_name)
			{
				idx = i;
				break;
			}
		}
		return unitMusters [idx].getLeader ();
	}
	
	public string addMuster(Hero obj)
	{
		if(!canMakeMuster()) return "none";
		
		string str = avaliableMuster ();
		
		unitMusters [currentMusterSize++].setName (str);
		
		addUnit (str, obj);
		
		return str;
	}
	
	public bool addUnits(string muster_name, string obj)
	{
		int objIdx = -1;
		string objName = obj;
		for(int i=0;i<MaxMusterCount;i++)
		{
			if(unitMusters[i].getName() == obj)
			{
				objIdx = i;
				break;
			}
		}
		if(objIdx < 0) return false;
		List<Hero> hero_list = new List<Hero> ();
		
		for(int i=0;i<MaxMusterCount;i++)
		{
			if(unitMusters[i].getName() == muster_name)
			{
				if(unitMusters[i].CountUnit() + unitMusters[objIdx].CountUnit() > 5)
				{
					Debug.Log("add units fail over counter");
					return false;
				}

				for(int k=0;k<MaxMusterUnitCount;k++)
				{
					Hero temp = unitMusters[objIdx].getElement(k);
					if(temp != null) hero_list.Add(temp);
				}
				objIdx = i;
				break;
			}
		}
		//first muster remove...
		freeMuster (objName);

		//second muster add...
		for(int i=0;i<hero_list.Count;i++)
		{
			hero_list[i].controller.setMusterID(muster_name);
			hero_list[i].controller.setMusterLeaderTrigger(false);
			hero_list[i].controller.setMusterTrigger(true);

			unitMusters[objIdx].addUnit(hero_list[i]);
		}
		hero_list.Clear ();
		hero_list = null;

		return true;
	}
	
	public bool addUnit(string muster_name, Hero obj)
	{
		for(int i=0;i<MaxMusterCount;i++)
		{
			if(unitMusters[i].getName() == muster_name)
			{
				if(unitMusters[i].CountUnit() >= MaxMusterUnitCount)
				{
					Debug.Log(unitMusters[i].getName() + " : " + unitMusters[i].CountUnit());
					return false;
				}
				obj.controller.setMusterID(muster_name);
				if(unitMusters[i].CountUnit() <= 0) obj.controller.setMusterLeaderTrigger(true);
				else obj.controller.setMusterLeaderTrigger(false);
				obj.controller.setMusterTrigger(true);

				unitMusters[i].addUnit(obj);
				return true;
			}
		}
		return false;
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
		Hero[] obj;
		int CurrentSize;
		Hero leader;
		
		string name;
		
		public void Initialize()
		{
			obj = new Hero[MaxMusterUnitCount]; //max count
			for(int i=0;i<MaxMusterUnitCount;i++) obj[i] = null;
			CurrentSize = 0;
		}
		
		public void freeMuster()
		{
			for(int i=0;i<MaxMusterUnitCount;i++)
			{
				if(obj[i] == null) continue;

				obj[i].controller.setMusterID("null");
				obj[i].controller.setMusterLeaderTrigger(false);
				obj[i].controller.setMusterTrigger(false);
				
				obj[i] = null;
				CurrentSize = 0;
			}
		}
		
		public void addUnit(Hero gobj)
		{
			if(CurrentSize >= MaxMusterUnitCount) return;
			
			for(int i=0;i<MaxMusterUnitCount;i++)
			{
				if(obj[i] == null)
				{
					obj[i] = gobj;
					obj[i].muster_name = name;
					if(obj[i].model.MusterLeader) leader = obj[i];
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
					if(obj[i].model.Name == nameID)
					{
						//change parent or remove
						//...
						CurrentSize--;
						if(obj[i].model.MusterLeader) leader = null;
						obj[i].controller.setMusterID("null");
						obj[i].controller.setMusterLeaderTrigger(false);
						obj[i].controller.setMusterTrigger(false);
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
		
		public Hero getElement(int idx)
		{
			return obj [idx];
		}
		
		public Hero getLeader()
		{
			return leader;
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
