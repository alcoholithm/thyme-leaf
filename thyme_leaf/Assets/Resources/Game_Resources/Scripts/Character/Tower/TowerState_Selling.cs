﻿using UnityEngine;
using System.Collections;

public class TowerState_Selling : State<Tower>
{
    private TowerState_Selling()
    {
        Successor = TowerState_Hitting.Instance;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Enter(Tower owner)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Execute(Tower owner)
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

    /// <summary>
    /// 
    /// </summary>
    private static TowerState_Selling instance = new TowerState_Selling(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static TowerState_Selling Instance
    {
        get { return TowerState_Selling.instance; }
        set { TowerState_Selling.instance = value; }
    }
}
