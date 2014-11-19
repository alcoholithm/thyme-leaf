using UnityEngine;
using System.Collections;

public class TowerState_Hitting : State<Tower> {

    private static TowerState_Hitting instance = new TowerState_Hitting(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Hitting Instance
    {
        get { return TowerState_Hitting.instance; }
        set { TowerState_Hitting.instance = value; }
    }

    private TowerState_Hitting()
    {
        Successor = TowerState_Hitting.Instance;
    }

    public override void Enter(Tower owner)
    {
        throw new System.NotImplementedException();
    }

    public override void Execute(Tower owner)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(Tower owner)
    {
        throw new System.NotImplementedException();
    }

    public override bool IsHandleable()
    {
        throw new System.NotImplementedException();
    }
}
