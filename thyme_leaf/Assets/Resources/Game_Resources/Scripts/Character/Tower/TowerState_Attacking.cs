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
     * followings are member functions
     */ 
    private void FindBestTarget(Tower owner)
    {
        // 초기에 이애가 죽었는지 살았는지 판단해야함.
        // 다른 놈에 의해 제거될 가능성도 있으므로
        if (owner.Enemies.Count > 0)
        {
            owner.CurrentTarget = owner.Enemies[0];
        }
    }

    /*
     * followings are overrided methods
     */
    public override void Enter(Tower owner)
    {
        Debug.Log(TAG + " Enter");
        owner.PlayAnimation(animName);
        FindBestTarget(owner);

        owner.SetAttackable(true);
    }

    public override void Execute(Tower owner)
    {
        // 1. 매 프레임 최적의 타겟을 찾는다.
        // 이 함수를 통해서 에너미 리스트에 적이 죽었는지 판별한다.
        FindBestTarget(owner);

        // if there are no enemies anymore.
        if (owner.Enemies.Count == 0)
        {
            owner.ChangeState(TowerState_Idling.Instance);
            return;
        }
    }

    public override void Exit(Tower owner)
    {
        Debug.Log(TAG + " Exit");

        owner.SetAttackable(false);
    }

    public override bool HandleMessage(Message msg)
    {
        bool isHandleable = false;
        //switch (msg.what)
        //{
        //    case MessageTypes.MSG_ENEMY_LEAVE:
        //        isHandleable = true;
        //        break;
        //}

        return isHandleable;
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
