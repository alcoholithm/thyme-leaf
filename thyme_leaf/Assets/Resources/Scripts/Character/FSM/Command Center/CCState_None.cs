using UnityEngine;
using System.Collections;

public class CCState_None : State<W_Chat>
{
    private CCState_None()
    {
        Successor = CCState_Hitting.Instance;
    }

    /*
     * Followings are overrided methods of "State"
     */
    public override void Enter(W_Chat owner) { }
    public override void Execute(W_Chat owner) { }
    public override void Exit(W_Chat owner) { }
    public override bool HandleMessage(Message msg) {return false;}

    /*
     * Followings are attributes
     */
    private static CCState_None instance = new CCState_None(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static CCState_None Instance
    {
        get { return CCState_None.instance; }
        set { CCState_None.instance = value; }
    }

    public new const string TAG = "[CCState_None]";
}
