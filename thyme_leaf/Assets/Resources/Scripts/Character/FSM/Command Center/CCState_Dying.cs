using UnityEngine;
using System.Collections;

public class CCState_Dying : State<W_Chat>
{
    private string animName = "Tower_Dying_";

    private CCState_Dying()
    {
        Successor = CCState_Hitting.Instance;
    }


    /*
     * followings are overrided methods of "State"
     */
    public override void Enter(W_Chat owner)
    {
        Debug.Log(TAG + "Enter");
        owner.Anim.PlayOneShot(animName, new VoidFunction2<W_Chat>(x => x.ChangeState(CCState_None.Instance), owner));
    }

    public override void Execute(W_Chat owner)
    {
    }

    public override void Exit(W_Chat owner)
    {
        Debug.Log(TAG + "Exit");
        //TowerSpawner.Instance.Free(owner.gameObject);
    }

    public override bool HandleMessage(Message msg)
    {
        return false;
    }

    /*
     * for singleton
     */
    private static CCState_Dying instance = new CCState_Dying(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static CCState_Dying Instance
    {
        get { return CCState_Dying.instance; }
    }

    public new const string TAG = "[CCState_Dying]";
}
