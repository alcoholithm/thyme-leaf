using UnityEngine;
using System.Collections;

public class THouseState_None : State<THouse>
{
    private THouseState_None()
    {
        Successor = THouseState_Hitting.Instance;
    }

    /*
     * Followings are overrided methods of "State"
     */
    public override void Enter(THouse owner) { }
    public override void Execute(THouse owner) { }
    public override void Exit(THouse owner) { }
    public override bool HandleMessage(Message msg) {return false;}

    /*
     * Followings are attributes
     */
    private static THouseState_None instance = new THouseState_None(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static THouseState_None Instance
    {
        get { return THouseState_None.instance; }
        set { THouseState_None.instance = value; }
    }

    public new const string TAG = "[THouseState_None]";
}
