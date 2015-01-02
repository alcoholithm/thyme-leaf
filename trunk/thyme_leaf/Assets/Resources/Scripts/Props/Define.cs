using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Define
{
	public static List<MapDataStruct> pathNode;
	public static void PathDataDispose()
	{
		if(pathNode != null)
		{
			pathNode.Clear ();
			pathNode = null;
		}
	}

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

	public static int Select_Hero = 0;
}

public struct PathNodeOffsetStruct
{
	public int size;
	public OffsetStruct[] offset_st;

	public PathNodeOffsetStruct(int size)
	{
		this.size = size;
		offset_st = new OffsetStruct[size];
		for (int i=0; i<size; i++) offset_st [i].Initialize ();
	}
	public void Dispose()
	{
		size = 0;
		for (int i=0; i<size; i++) offset_st [i].Initialize ();
	}

	public OffsetStruct getNodeOffset()
	{
		OffsetStruct data = new OffsetStruct ();
		for(int i=0;i<size;i++)
		{
			if(!offset_st[i].is_use) 
			{
				offset_st[i].is_use = true;
				offset_st[i].idx = i;
				data = offset_st[i];
				break;
			}
		}
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

	public void setOffsetPos(int idx, float x, float y)
	{
		offset_st [idx].Initialize (false, new Vector3 (x, y, 0));
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
