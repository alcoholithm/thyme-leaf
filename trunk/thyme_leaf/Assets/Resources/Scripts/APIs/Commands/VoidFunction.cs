using UnityEngine;
using System;
using System.Collections;

public class VoidFunction : ICommand
{
    private Action action;

    public VoidFunction(Action callback)
    {
        this.action = callback;
    }

    public void Execute()
    {
        action();
    }
}
