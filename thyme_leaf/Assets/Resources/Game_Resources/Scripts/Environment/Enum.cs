using UnityEngine;
using System.Collections;

public enum MoveModeState
{
	FORWARD = 0, BACKWARD
}

public enum DirectionOption
{
	TO_NEXT = 0, TO_PREV, TO_TURNROOT, TO_TURNLIST
}

public enum EditorState
{
	ADD = 0, REMOVE, CONNECT, SELECT
}

public enum PosParamOption
{
	CURRENT = 0, NEXT, PREVIOUS, TURNOFFROOT
}

public enum EnableNodeMode
{
	START_NODE = 0, END_NODE, TURNOFF_NODE
}

public enum SpriteList
{
	END = 0, NORMAL, START, TURNOFF
}

public enum StartPoint
{
	AUTOMART_POINT = 0, TROVANT_POINT
}

public enum UnitType
{
	AUTOMART_CHARACTER = 9 ,TROVANT_CHARACTER, 
	AUTOMART_TOWER, TROVANT_TOWER
}