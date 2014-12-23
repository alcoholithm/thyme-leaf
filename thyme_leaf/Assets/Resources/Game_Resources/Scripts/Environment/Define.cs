﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Define
{
	public static List<MapDataStruct> pathNode;
	public static void PathDataDispose()
	{
		pathNode.Clear ();
		pathNode = null;
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

	private static UnitType myType;
	public static void SetUnitType(UnitType option)
	{
		myType = option;
	}
	public static UnitType GetUnitType()
	{
		return myType;
	}

	public static int Select_Hero = 0;
}
