using UnityEngine;
using System.Collections;

public class THouseState_Hitting : State<THouse>
{
    //private string animName = "Tower_Hitting_";

    private THouseState_Hitting()
    {
        Successor = null;
    }

    /*
     * followings are overrided methods of "State"
     */
    // 여긴 불리지 않을꺼야
    public override void Enter(THouse owner)
    {
        Debug.Log(TAG + " Enter");
    }

    // -주의- 전역상태이기 때문에 계속 실행된다.
    public override void Execute(THouse owner)
    {
    }

    public override void Exit(THouse owner)
    {
        Debug.Log(TAG + " Exit");
    }

    public override bool HandleMessage(Message msg)
    {
		Debug.Log ("aaaaaaaaa : " + msg.arg1);
        switch (msg.what)
        {
            case MessageTypes.MSG_NORMAL_DAMAGE:
                (msg.receiver as THouse).TakeDamage(msg.arg1);
                return true;
        }
        return false;
    }

    /*
     * for singleton
     */
    public new const string TAG = "[THouseState_Hitting]";
    private static THouseState_Hitting instance = new THouseState_Hitting(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static THouseState_Hitting Instance
    {
        get { return instance; }
        set { instance = value; }
    }
}
