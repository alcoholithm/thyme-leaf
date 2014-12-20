using UnityEngine;
using System.Collections;

public class TowerState_None : State<Agt_Type1>
{
    private TowerState_None()
    {
        Successor = TowerState_Hitting.Instance;
    }

    public override void Enter(Agt_Type1 owner)
    {
    }
    public override void Execute(Agt_Type1 owner)
    {
    }
    public override void Exit(Agt_Type1 owner)
    {
    }

    public override bool HandleMessage(Message msg)
    {
        return false;
    }

    public new const string TAG = "[TowerState_None]";
    private static TowerState_None instance = new TowerState_None(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_None Instance
    {
        get { return TowerState_None.instance; }
        set { TowerState_None.instance = value; }
    }
}