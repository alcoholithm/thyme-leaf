using UnityEngine;
using System.Collections;

public class NullState : State<ATT_Type1>
{
    public override void Enter(ATT_Type1 owner) { }
    public override void Execute(ATT_Type1 owner) { }
    public override void Exit(ATT_Type1 owner) { }
    public override bool HandleMessage(Message msg) { return false; }

    /*
     * 
     */
    public new const string TAG = "[NullState]";
    private static NullState instance = new NullState(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static NullState Instance
    {
        get { return NullState.instance; }
        set { NullState.instance = value; }
    }
}