using UnityEngine;
using System.Collections;

public class WChatState_Hitting : State<WChat>
{
    //private string animName = "Tower_Hitting_";

    private WChatState_Hitting()
    {
        Successor = null;
    }

    /*
     * followings are overrided methods of "State"
     */
    // 여긴 불리지 않을꺼야
    public override void Enter(WChat owner)
    {
        Debug.Log(TAG + " Enter");
    }

    // -주의- 전역상태이기 때문에 계속 실행된다.
    public override void Execute(WChat owner)
    {
    }

    public override void Exit(WChat owner)
    {
        Debug.Log(TAG + " Exit");
    }

    public override bool HandleMessage(Message msg)
    {
        switch (msg.what)
        {
            case MessageTypes.MSG_DAMAGE:
                (msg.receiver as WChat).TakeDamage(msg.arg1);
                return true;
        }
        return false;
    }

    /*
     * for singleton
     */
    public new const string TAG = "[WChatState_Hitting]";
    private static WChatState_Hitting instance = new WChatState_Hitting(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static WChatState_Hitting Instance
    {
        get { return instance; }
        set { instance = value; }
    }
}
