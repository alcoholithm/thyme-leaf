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
	AUTOMAT_NODE = 0, TROVANT_NODE, TURNOFF_NODE
}

public enum SpriteList
{
	TROVANT = 0, NORMAL, AUTOMAT, TURNOFF
}

public enum StartPoint
{
	AUTOMART_POINT = 0, TROVANT_POINT
}

public enum UnitType
{
	AUTOMART_CHARACTER = 9 ,TROVANT_CHARACTER = 10, 
	AUTOMART_TOWER = 11, AUTOMAT_PROJECTILE = 12
}

public enum AutomatType
{
    FRANSIS_TYPE1 = 0,

}

public enum TrovantType
{
    COMMA = 0,
}

public enum ProjectileType
{
    POISON = 0,
}

public enum TowerType
{
    APT = 0,
}

public enum FindingNodeDefaultOption
{
	NORMAL = 0, RANDOM_NODE, MUSTER_COMMAND
}

public class RPCMethod
{
    public const string INIT_SPAWNED_OBJECT = "INIT_SPAWNED_OBJECT";
}
