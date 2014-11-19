using UnityEngine;
using System.Collections;

public class TowerState_Selling : State<Tower>
{
    private static TowerState_Selling instance = new TowerState_Selling(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Selling Instance
    {
        get { return TowerState_Selling.instance; }
        set { TowerState_Selling.instance = value; }
    }

    private TowerState_Selling() { }

    public void Enter(Tower owner)
    {
        throw new System.NotImplementedException();
    }

    public void Execute(Tower owner)
    {
        throw new System.NotImplementedException();
    }

    public void Exit(Tower owner)
    {
        throw new System.NotImplementedException();
    }
}
