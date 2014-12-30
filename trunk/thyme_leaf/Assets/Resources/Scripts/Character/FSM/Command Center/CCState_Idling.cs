using UnityEngine;
using System.Collections;

public class CCState_Idling : State<W_Chat>
{
    //private string animName = "Comma_Attacking_Normal_";

    private CCState_Idling()
    {
        Successor = CCState_Hitting.Instance;
    }

    public override void Enter(W_Chat owner)
    {
        Debug.Log(TAG + " Enter");
        //owner.Anim.Play(animName);
    }

    public override void Execute(W_Chat owner)
    {

    }

    public override void Exit(W_Chat owner)
    {
        Debug.Log(TAG + " Exit");
    }

    public override bool HandleMessage(Message msg)
    {
        throw new System.NotImplementedException();
    }

    private static CCState_Idling instance = new CCState_Idling();
    public static CCState_Idling Instance
    {
        get { return CCState_Idling.instance; }
        set { CCState_Idling.instance = value; }
    }

    public new const string TAG = "[CCState_Idling]";
}
