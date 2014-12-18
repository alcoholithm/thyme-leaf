using UnityEngine;
using System;
using System.Collections;

public class VoidFunction5<TArg1, TArg2, TArg3, TArg4> : ICommand
{
    private Action<TArg1, TArg2, TArg3, TArg4> action;
    private TArg1 arg1;
    private TArg2 arg2;
    private TArg3 arg3;
    private TArg4 arg4;

    public VoidFunction5(Action<TArg1, TArg2, TArg3, TArg4> callback, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
    {
        this.action = callback;
        this.arg1 = arg1;
        this.arg2 = arg2;
        this.arg3 = arg3;
        this.arg4 = arg4;
    }

    public void Execute()
    {
        action(arg1, arg2, arg3, arg4);
    }
}
