using UnityEngine;
using System.Collections;

public class NullState : State<Tower>
{
    public override void Enter(Tower owner) { }
    public override void Execute(Tower owner) { }
    public override void Exit(Tower owner) { }
    public override bool IsHandleable(Message msg) { return false; }

    /// <summary>
    /// 
    /// </summary>
    public new const string TAG = "[TowerState_Building]";
    private static NullState instance = new NullState(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static NullState Instance
    {
        get { return NullState.instance; }
        set { NullState.instance = value; }
    }
}