using UnityEngine;
using System.Collections;

public class TowerState_Attacking : State<Tower>
{
    private string animName = "Tower_Attacking_";

    private TowerState_Attacking()
    {
        Successor = TowerState_Hitting.Instance;
    }

    /*
     * followings are overrided methods
     */
    public override void Enter(Tower owner)
    {
        Debug.Log("TowerState_Attacking start");
        owner.PlayAnimation(animName);
    }

    public override IEnumerator Execute(Tower owner)
    {
        //FindBestTarget();

        // 최적의 타겟을 찾고 공격을 한다.
        // 공격주기마다 상대의 체력을 깍는 메시지를 보낸다. MessageTypes.MSG_DAMAGE
		return null;
    }

    public override void Exit(Tower owner)
    {
        Debug.Log("TowerState_Attacking end");
    }

    public override bool IsHandleable(Message msg)
    {
        switch (msg.what)
        {
            case MessageTypes.MSG_ENEMY_LEAVE:
                return true;
        }

        return false;
    }

    /*
     * for singleton
     */
    public new const string TAG = "[TowerState_Attacking]";
    private static TowerState_Attacking instance = new TowerState_Attacking(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Attacking Instance
    {
        get { return TowerState_Attacking.instance; }
    }
}
