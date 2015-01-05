using UnityEngine;
using System.Collections;

public class TowerState_Attacking : State<Agt_Type1>
{
    private string animName = "Tower_Attacking_";
    //private string animName = "APT_Type1_Attacking_";

    private TowerState_Attacking()
    {
        Successor = TowerState_Hitting.Instance;
    }

   

    /*
     * followings are overrided methods
     */
    public override void Enter(Agt_Type1 owner)
    {
        Debug.Log(TAG + " Enter");

        owner.Anim.Play(animName);

        owner.SetAttackable(true);
    }

    public override void Execute(Agt_Type1 owner)
    {
        // if there are no enemies anymore.
        owner.Model.FindBestTarget();

        if (owner.Model.Enemies.Count == 0)
        {
            owner.ChangeState(TowerState_Idling.Instance);
            return;
        }
    }

    public override void Exit(Agt_Type1 owner)
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
