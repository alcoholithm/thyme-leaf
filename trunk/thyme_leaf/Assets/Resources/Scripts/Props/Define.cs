﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Define
{
	public static List<MapDataStruct> pathNode = null;
	public static void PathDataDispose()
	{
		if(pathNode != null)
		{
			pathNode.Clear ();
			pathNode = null;
		}
	}

	public static List<GameObject> THouse_list = null;
	public static void CenterListDisPose()
	{
		if(THouse_list != null)
		{
			THouse_list.Clear();
			THouse_list = null;
		}
	}

	public static Vector3 selected_center;

	public static List<GameObject> automat_center_node = null;  //exception procc ~...
	public static List<GameObject> trovant_center_node = null;

	public static float RadianToAngle()
	{
		return 57.2957f;
	}

	public static float AngleToRadian()
	{
		return 0.017453f;
	}

	public static int TurnOffMaxCount()
	{
		return 4;
	}

	public static float FrameControl()
	{
		return Time.deltaTime * 60;
	}

	public static PathNodeOffsetStruct path_node_off;

	public static RandomTableStruct random_index_table;

	public static StageCenterSetStruct[] stage_wave_sys_setting;

	public static int Select_Hero = 0;

	public static int current_stage_number;
}

public struct RandomTableStruct
{
	public RandomIdxStruct[] rand_table;
	public int size;
	public int count_rand;

	public RandomTableStruct(int size, int min_n, int max_n)
	{
		this.size = size;
		rand_table = new RandomIdxStruct[size];
		for(int i=0;i<size;i++)
			rand_table[i].Initialize(Random.Range(min_n, max_n+1), false);
		count_rand = 0;
	}

	public void Initialize(int size, int min_n, int max_n)
	{
		this.size = size;
		rand_table = new RandomIdxStruct[size];
		for(int i=0;i<size;i++)
			rand_table[i].Initialize(Random.Range(min_n, max_n+1), false);
		count_rand = 0;
	}

	public int getRandomIndex()
	{
		int idx = (size + count_rand) % size;
		int R = rand_table [idx].idx;
		count_rand++;
		return R;
	}
}

public struct RandomIdxStruct
{
	public int idx;
	public bool isUse;

	public RandomIdxStruct(int idx, bool use)
	{
		this.idx = idx;
		isUse = use;
	}
	public void Initialize(int idx, bool use)
	{
		this.idx = idx;
		isUse = use;
	}

	public void Initialize()
	{
		idx = -1;
		isUse = false;
	}
}

public struct PathNodeOffsetStruct
{
	private int size;
	public OffsetStruct[] offset_st;
	public int count_num;

	public PathNodeOffsetStruct(int size)
	{
		this.size = size;
		offset_st = new OffsetStruct[size];
		for (int i=0; i<size; i++) offset_st [i].Initialize ();
		count_num = 0;
	}
	public void Dispose()
	{	
		size = 0;
		for (int i=0; i<size; i++) offset_st [i].Initialize ();
		count_num = 0;
	}

	public OffsetStruct getNodeOffset()
	{
		OffsetStruct data = new OffsetStruct ();
		int i = (count_num + size) % size; 
		offset_st[i].is_use = true;
		offset_st[i].idx = i;
		data = offset_st[i];
		count_num++;
		return data;
	}

	public void OffsetRelease(OffsetStruct off)
	{
		int idx = off.idx;
		offset_st [idx].Initialize ();
	}

	public Vector3 getOffsetPos(int idx)
	{
		return offset_st [idx].offset;
	}

	public void setOffsetPos(int idx, float x, float y, float scale)
	{
		offset_st [idx].Initialize (false, (new Vector3 (x, y, 0) * scale));
	}
}

public struct OffsetStruct
{
	public int idx;
	public bool is_use;
	public Vector3 offset;

	public OffsetStruct(bool use, Vector3 v)
	{
		is_use = use;
		offset = v;
		idx = -1;
	}
	
	public void Initialize(bool use, Vector3 v)
	{
		is_use = use;
		offset = v;
		idx = -1;
	}

	public void Initialize()
	{
		is_use = false;
		offset = Vector3.zero;
		idx = -1;
	}
}


