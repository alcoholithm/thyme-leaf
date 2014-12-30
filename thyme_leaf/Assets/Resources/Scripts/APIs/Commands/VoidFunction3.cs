using UnityEngine;
using System;
using System.Collections;

public class VoidFunction3<TArg1, TArg2> : ICommand
{
    private Action<TArg1, TArg2> action;
    private TArg1 arg1;
    private TArg2 arg2;

    public VoidFunction3(Action<TArg1, TArg2> callback, TArg1 arg1, TArg2 arg2)
    {
        this.action = callback;
        this.arg1 = arg1;
        this.arg2 = arg2;
    }

    public void Execute()
    {
        action(arg1, arg2);
    }
}
