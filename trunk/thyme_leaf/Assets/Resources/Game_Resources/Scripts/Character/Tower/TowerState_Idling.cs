using UnityEngine;
using System.Collections;

public class TowerState_Idling : State<Tower>
{
    private string animName = "Tower_Idling_";

    private TowerState_Idling() 
    {
        Successor = TowerState_Hitting.Instance;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Enter(Tower owner)
    {
        Debug.Log("TowerState_Idling start");
        owner.PlayAnimation(animName);
    }

    public override void Execute(Tower owner)
    {
        Debug.Log("Isersfs");
    }

    public override void Exit(Tower owner)
    {
        Debug.Log("TowerState_Idling end");
    }

    public override bool IsHandleable(Message msg)
    {
        switch (msg.what)
        {
            case MessageTypes.MSG_ENEMY_ENTER:
                return true;
        }

        return false;
    }

    /*
     * 
     */
    public new const string TAG = "[TowerState_Idling]";
    private static TowerState_Idling instance = new TowerState_Idling(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Idling Instance
    {
        get { return TowerState_Idling.instance; }
        set { TowerState_Idling.instance = value; }
    }
}
