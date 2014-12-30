using UnityEngine;
using System.Collections;

public class NullState<T> : State<T>
{
    public override void Enter(T owner) { }
    public override void Execute(T owner) { }
    public override void Exit(T owner) { }
    public override bool HandleMessage(Message msg) { return false; }

    /*
     * Followings are attributes
     */
    private static NullState<T> instance = new NullState<T>(); // lazy 하게 생성해준다고 한다. 믿어 봐야지 뭐
    public static NullState<T> Instance
    {
        get { return NullState<T>.instance; }
        set { NullState<T>.instance = value; }
    }
    public new const string TAG = "[NullState]";
}