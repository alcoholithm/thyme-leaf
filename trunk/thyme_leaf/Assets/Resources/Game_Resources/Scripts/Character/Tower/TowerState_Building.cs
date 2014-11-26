using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class TowerState_Building : State<Tower>
{
    private string animName = "Tower_Building_";

    private float buildingTime = 5.0f;

    private TowerState_Building()
    {
        Successor = TowerState_Hitting.Instance;
    }

    /// <summary>
    /// 
    /// </summary>
    IEnumerator BuildTower(Tower owner)
    {
        owner.PlayAnimation(animName);
        yield return new WaitForSeconds(buildingTime);
        owner.StateMachine.ChangeState(TowerState_Idling.Instance);
    }
    
    /// <summary>
    /// followings are 
    /// </summary>
    public override void Enter(Tower owner)
    {
        Debug.Log("TowerState_Building start");
        owner.StartCoroutine(BuildTower(owner));
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
        Debug.Log(TAG + "IsHandleable");
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    public new const string TAG = "[TowerState_Building]";
    private static TowerState_Building instance = new TowerState_Building(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Building Instance
    {
        get { return TowerState_Building.instance; }
        set { TowerState_Building.instance = value; }
    }
}
