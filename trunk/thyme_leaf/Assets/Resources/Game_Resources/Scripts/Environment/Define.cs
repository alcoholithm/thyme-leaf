using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Define
{
	public static List<GameObject> pathNode;
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
	/*
	public static string NodeSpriteFileName(SpriteList option)
	{
		switch(option)
		{
		case SpriteList.END:
			return "End_Point";
		case SpriteList.NORMAL:
			return "Normal_Point";
		case SpriteList.START:
			return "Start_Point";
		case SpriteList.TURNOFF:
			return "TurnOff_Point";
		}
	}
	*/
}
