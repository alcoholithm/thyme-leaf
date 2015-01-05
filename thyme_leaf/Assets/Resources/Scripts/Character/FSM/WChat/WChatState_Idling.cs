using UnityEngine;
using System.Collections;

public class WChatState_Idling : State<WChat>
{
    private string animName = "Tower_Hitting_";

    private WChatState_Idling()
    {
        Successor = WChatState_Hitting.Instance;
    }

    /*
     * followings are overrided methods of "State"
     */
    public override void Enter(WChat owner)
    {
        Debug.Log(TAG + " Enter");
        owner.Anim.Play(animName);
    }

    public override void Execute(WChat owner)
    {

    }

    public override void Exit(WChat owner)
    {
        Debug.Log(TAG + " Exit");
    }

    public override bool HandleMessage(Message msg)
    {
        return false;
    }

    private static WChatState_Idling instance = new WChatState_Idling();
    public static WChatState_Idling Instance
    {
        get { return WChatState_Idling.instance; }
        set { WChatState_Idling.instance = value; }
    }

    public new const string TAG = "[WChatState_Idling]";
}
