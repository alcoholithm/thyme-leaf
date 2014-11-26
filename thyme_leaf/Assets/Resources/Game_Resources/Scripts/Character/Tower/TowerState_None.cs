using UnityEngine;
using System.Collections;

public class TowerState_None : State<Tower>
{
    private TowerState_None()
    {
        Successor = TowerState_Hitting.Instance;
    }

    public override void Enter(Tower owner)
    {
    }
    public override void Execute(Tower owner)
    {
    }
    public override void Exit(Tower owner)
    {
    }

    public override bool IsHandleable(Message msg)
    {
        Debug.Log(TAG + "IsHandleable");
        switch (msg.what)
        {
            case MessageTypes.MSG_BUILD_TOWER:
                return true;
        }

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