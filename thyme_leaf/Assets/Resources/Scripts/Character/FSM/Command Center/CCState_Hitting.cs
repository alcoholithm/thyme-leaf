using UnityEngine;
using System.Collections;

public class CCState_Hitting : State<W_Chat>
{
    //private string animName = "Tower_Hitting_";

    private CCState_Hitting()
    {
        Successor = null;
    }

    /*
     * followings are overrided methods of "State"
     */
    // 여긴 불리지 않을꺼야
    public override void Enter(W_Chat owner)
    {
        Debug.Log(TAG + " Enter");
    }

    // -주의- 전역상태이기 때문에 계속 실행된다.
    public override void Execute(W_Chat owner)
    {
    }

    public override void Exit(W_Chat owner)
    {
        Debug.Log(TAG + " Exit");
    }

    public override bool HandleMessage(Message msg)
    {
        switch (msg.what)
        {
            case MessageTypes.MSG_DAMAGE:
                (msg.receiver as W_Chat).TakeDamage(msg.arg1);
                return true;
        }
        return false;
    }

    /*
     * for singleton
     */
    public new const string TAG = "[CCState_Hitting]";
    private static CCState_Hitting instance = new CCState_Hitting(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static CCState_Hitting Instance
    {
        get { return instance; }
        set { instance = value; }
    }
}
