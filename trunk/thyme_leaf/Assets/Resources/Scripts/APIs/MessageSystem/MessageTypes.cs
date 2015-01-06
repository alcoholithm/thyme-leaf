using UnityEngine;
using System.Collections;

/// <summary>
/// MessageType description
/// </summary>
/// 
/// <remarks>
/// MSG_HI : ~~~
/// 
/// </remarks>s
public enum MessageTypes {
    MSG_NONE = -1,
    MSG_TOWER_READY,
	MSG_MOVE_HERO,
    MSG_NORMAL_DAMAGE,
    MSG_POISON_DAMAGE,
    MSG_BURN_DAMAGE,
	MSG_MISSING
}
