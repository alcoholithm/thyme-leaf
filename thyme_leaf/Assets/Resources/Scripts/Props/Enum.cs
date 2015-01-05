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
	AUTOMAT_POINT = 0, TROVANT_POINT
}

public enum UnitType
{
    AUTOMAT_WCHAT = 8,
	AUTOMAT_CHARACTER = 9 ,TROVANT_CHARACTER = 10, 
	AUTOMAT_TOWER = 11, AUTOMAT_PROJECTILE = 12,
    TROVANT_THOUSE = 13, 
}

public enum AudioUnitType
{
    //automats
    FRANSCIS_TYPE1,
    FALSTAFF_TYPE1,
    FOLTINBRAS_TYPE1,
    VICTOR_TYPE1,
    MARIEN_TYPE1,

    //towers
    APT,
    AGT,
    AST,
    ASPT,
    ATT,

    //projectiles
    POISON,

    //wchats
    WCHAT_TYPE1,

    //trovants
    COMMA,
    PYTHON,

    //thouse
    THOUSE_TYPE1,
}

public enum AutomatType
{
    FRANSIS_TYPE1 = 0,
    FALSTAFF_TYPE1,    
}

public enum TrovantType
{
    COMMA = 0,
    PYTHON,
}

public enum ProjectileType
{
    POISON = 0,
}

public enum TowerType
{
    APT = 0,
    AGT, // Automat Gun Tower
    AST, // Automat Slow Tower
    ASPT, // Automat Splash Tower
    ATT, // Automat Turret Tower
}

public enum THouseType
{
    THOUSE_TYPE1 = 0,
}

public enum WChatType // Walkalbe Commanding Headquater Against Trovant
{
    WCHAT_TYPE1 = 0,
}

public enum MusicType
{
    LOBBY = 0,
    BATTLE_1 = 1,
}

public enum SoundType
{
    AUTOMAT_FRANSIS_TYPE1_ATTACKING = 0,
    AUTOMAT_FRANSIS_TYPE1_DYING,

    AUTOMAT_FALSTAFF_TYPE1_ATTACKING,
    AUTOMAT_FALSTAFF_TYPE1_DYING,

    AUTOMAT_APT_TYPE1_ATTACKING,
    AUTOMAT_APT_TYPE1_DYING,

    TROVANT_COMMA_ATTACKING,
    TROVANT_COMMA_DYING,

    TROVANT_PYTHON_ATTACKING,
    TROVANT_PYTHON_DYING,
}

public enum FindingNodeDefaultOption
{
	NORMAL = 0, RANDOM_NODE, MUSTER_COMMAND, OTHER
}

