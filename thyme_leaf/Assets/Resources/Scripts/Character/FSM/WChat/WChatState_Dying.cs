using UnityEngine;
using System.Collections;

public class WChatState_Dying : State<WChat>
{
    private string animName = "Tower_Dying_";

    private WChatState_Dying()
    {
        Successor = WChatState_Hitting.Instance;
    }


    /*
     * followings are overrided methods of "State"
     */
    public override void Enter(WChat owner)
    {
        Debug.Log(TAG + "Enter");
        owner.Anim.PlayOneShot(animName, new VoidFunction2<WChat>(x => x.ChangeState(WChatState_None.Instance), owner));
    }

    public override void Execute(WChat owner)
    {
    }

    public override void Exit(WChat owner)
    {
        Debug.Log(TAG + "Exit");

        // 오토마트측에서 얘가 부서진 것만 알면 된다.
        // 모델은 오토마트가 되어야 하며 오토마트 모델에서 타워라든지 기타 등등을 관리한다.

        Spawner.Instance.Free(owner.gameObject);
    }

    public override bool HandleMessage(Message msg)
    {
        return false;
    }

    /*
     * for singleton
     */
    private static WChatState_Dying instance = new WChatState_Dying(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static WChatState_Dying Instance
    {
        get { return WChatState_Dying.instance; }
    }

    public new const string TAG = "[WChatState_Dying]";
}
