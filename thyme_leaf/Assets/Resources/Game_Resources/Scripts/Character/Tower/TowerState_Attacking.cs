﻿using UnityEngine;
using System.Collections;

public class TowerState_Attacking : State<Tower>
{
    private TowerState_Attacking()
    {
        Successor = TowerState_Hitting.Instance;
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
        throw new System.NotImplementedException();
    }

    /*
     * for singleton
     */
    public new const string TAG = "[TowerState_Attacking]";
    private static TowerState_Attacking instance = new TowerState_Attacking(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Attacking Instance
    {
        get { return TowerState_Attacking.instance; }
    }
}
