using UnityEngine;
using System.Collections;

public class TowerState_Hitting : State<Tower>
{
    private TowerState_Hitting()
    {
        Successor = null;
    }

    /*
     * followings are overrided methods
     */ 
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

    public override bool IsHandleable(Message msg)
    {
        switch (msg.what)
        {
            case MessageTypes.MSG_DAMAGE:
                return true;
        }

        return false;
    }

    /*
     * for singleton
     */ 
    public new const string TAG = "[TowerState_Hitting]";
    private static TowerState_Hitting instance = new TowerState_Hitting(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Hitting Instance
    {
        get { return instance; }
        set { instance = value; }
    }
}
