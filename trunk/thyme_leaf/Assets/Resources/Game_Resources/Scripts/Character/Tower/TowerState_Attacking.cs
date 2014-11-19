﻿using UnityEngine;
using System.Collections;

public class TowerState_Attacking : State<Tower>
{
    private static TowerState_Attacking instance = new TowerState_Attacking(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Attacking Instance
    {
        get { return TowerState_Attacking.instance; }
        set { TowerState_Attacking.instance = value; }
    }

    private TowerState_Attacking() { }

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
