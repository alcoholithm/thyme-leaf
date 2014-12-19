using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class TowerState_Building : State<ATT_Type1>
{
    private string animName = "Tower_Building_";

    private float buildingTime = 1.5f;

    private TowerState_Building()
    {
        Successor = TowerState_Hitting.Instance;
    }

    /*
     * followings are member functions
     */ 

    /*
     * followings are implemented methods of interface
     */ 
    public override void Enter(ATT_Type1 owner)
    {
        Debug.Log(TAG + " Enter");

        owner.Anim.Play(animName);
        Message msg = owner.ObtainMessage(MessageTypes.MSG_TOWER_READY, new TowerReadyCommand(owner));
        owner.DispatchMessageDelayed(msg, buildingTime);
    }

    public override void Execute(ATT_Type1 owner)
    {
    }

    public override void Exit(ATT_Type1 owner)
    {
        Debug.Log(TAG + " Exit");
    }

    public override bool HandleMessage(Message msg)
    {
        switch (msg.what)
        {
            case MessageTypes.MSG_TOWER_READY:
                msg.command.Execute();
                return true;
        }

        return false;
    }

    /*
     * Attributes
     */ 
    public new const string TAG = "[TowerState_Building]";
    private static TowerState_Building instance = new TowerState_Building(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Building Instance
    {
        get { return TowerState_Building.instance; }
        set { TowerState_Building.instance = value; }
    }

}
