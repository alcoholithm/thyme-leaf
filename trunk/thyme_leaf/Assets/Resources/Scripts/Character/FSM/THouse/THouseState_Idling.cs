using UnityEngine;
using System.Collections;

public class THouseState_Idling : State<THouse>
{
    private string animName = "Tower_Attacking_";

    private THouseState_Idling()
    {
        Successor = THouseState_Hitting.Instance;
    }

    /*
     * followings are overrided methods of "State"
     */
    public override void Enter(THouse owner)
    {
        Debug.Log(TAG + " Enter");
        owner.Anim.Play(animName);
    }

    public override void Execute(THouse owner)
    {

    }

    public override void Exit(THouse owner)
    {
        Debug.Log(TAG + " Exit");
    }

    public override bool HandleMessage(Message msg)
    {
        return false;
    }

    private static THouseState_Idling instance = new THouseState_Idling();
    public static THouseState_Idling Instance
    {
        get { return THouseState_Idling.instance; }
        set { THouseState_Idling.instance = value; }
    }

    public new const string TAG = "[THouseState_Idling]";
}
