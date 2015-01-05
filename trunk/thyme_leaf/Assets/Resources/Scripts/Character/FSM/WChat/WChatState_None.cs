using UnityEngine;
using System.Collections;

public class WChatState_None : State<WChat>
{
    private WChatState_None()
    {
        Successor = WChatState_Hitting.Instance;
    }

    /*
     * Followings are overrided methods of "State"
     */
    public override void Enter(WChat owner) { }
    public override void Execute(WChat owner) { }
    public override void Exit(WChat owner) { }
    public override bool HandleMessage(Message msg) {return false;}

    /*
     * Followings are attributes
     */
    private static WChatState_None instance = new WChatState_None(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static WChatState_None Instance
    {
        get { return WChatState_None.instance; }
        set { WChatState_None.instance = value; }
    }

    public new const string TAG = "[CCState_None]";
}
