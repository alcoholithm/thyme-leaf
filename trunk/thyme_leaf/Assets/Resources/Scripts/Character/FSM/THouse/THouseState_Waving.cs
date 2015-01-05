using UnityEngine;
using System.Collections;

public class THouseState_Waving : State<THouse>
{
    private string animName = "Tower_Attacking_";

    private THouseState_Waving()
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

    private static THouseState_Waving instance = new THouseState_Waving();
    public static THouseState_Waving Instance
    {
        get { return THouseState_Waving.instance; }
        set { THouseState_Waving.instance = value; }
    }

    public new const string TAG = "[THouseState_Waving]";
}
