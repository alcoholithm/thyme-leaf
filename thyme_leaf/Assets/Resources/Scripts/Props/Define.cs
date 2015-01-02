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
	public Vector3[] offset;

	public PathNodeOffsetStruct(int size)
	{
		this.size = size;
		offset = new Vector3[size];
	}
	public void Dispose()
	{
		size = 0;
		offset = null;
	}
}
