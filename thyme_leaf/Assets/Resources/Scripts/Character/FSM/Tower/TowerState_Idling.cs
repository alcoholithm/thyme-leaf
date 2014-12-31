using UnityEngine;
using System.Collections;

public class TowerState_Idling : State<Agt_Type1>
{
    private string animName = "APT_Type1_Idling_";

    private TowerState_Idling() 
    {
        Successor = TowerState_Hitting.Instance;
    }

    public override void Enter(Agt_Type1 owner)
    {
        Debug.Log(TAG + " Enter");
        owner.Anim.Play(animName);
    }

    public override void Execute(Agt_Type1 owner)
    {
        // 매 프레임마다 주변에 적이 있는지 아닌지 검사를 해 주어야 한다.
        // 마찬가지로 아군에게 죽어서 없어진 경우도 체크해줘야 한다.
        if (owner.Model.Enemies.Count > 0)
        {
            owner.ChangeState(TowerState_Attacking.Instance);
            return;
        }
    }

    public override void Exit(Agt_Type1 owner)
    {
        Debug.Log(TAG + " Exit");
    }

    public override bool HandleMessage(Message msg)
    {
        //switch (msg.what)
        //{
        //    case MessageTypes.MSG_ENEMY_ENTER:
        //        msg.command.Execute();
        //        return true;
        //}

        return false;
    }

    /*
     * Followings are Attributes
     */
    private static TowerState_Idling instance = new TowerState_Idling(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Idling Instance
    {
        get { return TowerState_Idling.instance; }
        set { TowerState_Idling.instance = value; }
    }
    public new const string TAG = "[TowerState_Idling]";
}
