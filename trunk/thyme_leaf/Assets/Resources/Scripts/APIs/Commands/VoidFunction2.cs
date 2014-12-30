using UnityEngine;
using System;
using System.Collections;

public class VoidFunction2<TArg> : ICommand
{
    private Action<TArg> action;
    private TArg arg;

    public VoidFunction2(Action<TArg> callback, TArg arg)
    {
        this.action = callback;
        this.arg = arg;
    }

    public void Execute()
    {
        action(arg);
    }
}
