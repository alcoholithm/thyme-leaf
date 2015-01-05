using UnityEngine;
using System.Collections;

public class THouseState_Dying : State<THouse>
{
    private string animName = "Tower_Dying_";

    private THouseState_Dying()
    {
        Successor = WChatState_Hitting.Instance;
    }


    /*
     * followings are overrided methods of "State"
     */
    public override void Enter(THouse owner)
    {
        Debug.Log(TAG + "Enter");
        owner.Anim.PlayOneShot(animName, new VoidFunction2<THouse>(x => x.ChangeState(THouseState_None.Instance), owner));
    }

    public override void Execute(THouse owner)
    {
    }

    public override void Exit(THouse owner)
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
    private static THouseState_Dying instance = new THouseState_Dying(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static THouseState_Dying Instance
    {
        get { return THouseState_Dying.instance; }
    }

    public new const string TAG = "[THouseState_Dying]";
}
