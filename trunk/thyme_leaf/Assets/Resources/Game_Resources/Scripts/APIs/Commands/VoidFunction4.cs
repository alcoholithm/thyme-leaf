using UnityEngine;
using System;
using System.Collections;

public class VoidFunction4<TArg1, TArg2, TArg3> : ICommand
{
    private Action<TArg1, TArg2, TArg3> action;
    private TArg1 arg1;
    private TArg2 arg2;
    private TArg3 arg3;

    public VoidFunction4(Action<TArg1, TArg2, TArg3> callback, TArg1 arg1, TArg2 arg2, TArg3 arg3)
    {
        this.action = callback;
        this.arg1 = arg1;
        this.arg2 = arg2;
        this.arg3 = arg3;
    }

    public void Execute()
    {
        action(arg1, arg2, arg3);
    }
}
