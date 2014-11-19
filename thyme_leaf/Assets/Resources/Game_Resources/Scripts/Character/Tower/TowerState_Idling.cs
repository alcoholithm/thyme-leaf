using UnityEngine;
using System.Collections;

public class TowerState_Idling : State<Tower>
{
    private static TowerState_Idling instance = new TowerState_Idling(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Idling Instance
    {
        get { return TowerState_Idling.instance; }
        set { TowerState_Idling.instance = value; }
    }

    private string animName = "Tower_Idling_";

    private TowerState_Idling() 
    {
        Successor = TowerState_Hitting.Instance;
    }

    public override void Enter(Tower owner)
    {
        Debug.Log("TowerState_Idling start");
        owner.PlayAnimation(animName);
    }

    public override void Execute(Tower owner)
    {
    }

    public override void Exit(Tower owner)
    {
        Debug.Log("TowerState_Idling end");
    }

    public override bool IsHandleable()
    {
        throw new System.NotImplementedException();
    }
}
