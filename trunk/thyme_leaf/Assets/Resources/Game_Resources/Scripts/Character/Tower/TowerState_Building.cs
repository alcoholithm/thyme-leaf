using UnityEngine;
using System.Collections;

public class TowerState_Building : State<Tower>
{
    private static TowerState_Building instance = new TowerState_Building(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Building Instance
    {
        get { return TowerState_Building.instance; }
        set { TowerState_Building.instance = value; }
    }

    private string animName = "Tower_Building_";

    private TowerState_Building()
    {
        Successor = TowerState_Hitting.Instance;
    }

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

    public override bool IsHandleable()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator BuildTower(Tower owner)
    {
        owner.PlayAnimation(animName);
        yield return new WaitForSeconds(5.0f);
        owner.StateMachine.ChangeState(TowerState_Idling.Instance);
    }

   
}
