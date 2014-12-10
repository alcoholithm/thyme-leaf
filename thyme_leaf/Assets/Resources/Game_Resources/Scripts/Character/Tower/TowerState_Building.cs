using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class TowerState_Building : State<Tower>
{
    private string animName = "Tower_Building_";

    private float buildingTime = 0.5f;

    private TowerState_Building()
    {
        Successor = TowerState_Hitting.Instance;
    }

    /*
     * followings are implemented methods of interface
     */ 
    public override void Enter(Tower owner)
    {
        Debug.Log("TowerState_Building start");

        owner.PlayAnimation(animName);

        Message msg = owner.ObtainMessage(MessageTypes.MSG_TOWER_READY, new TowerReadyCommand(owner));
        owner.DispatchMessageDelayed(msg, buildingTime);
    }

    public override void Execute(Tower owner)
    {
    }

    public override void Exit(Tower owner)
    {
        Debug.Log("TowerState_Building end");
    }

    public override bool IsHandleable(Message msg)
    {
        switch (msg.what)
        {
            case MessageTypes.MSG_TOWER_READY:
                return true;
        }

        return false;
    }

    /*
     *
     */ 
    public new const string TAG = "[TowerState_Building]";
    private static TowerState_Building instance = new TowerState_Building(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Building Instance
    {
        get { return TowerState_Building.instance; }
        set { TowerState_Building.instance = value; }
    }
}
