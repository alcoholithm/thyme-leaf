using UnityEngine;
using System.Collections;

public class NoCommand : ICommand
{
    public const string TAG = "[NoCommand]";

    public void Execute()
    {
        Debug.Log(TAG + "NoCommand");
    }
}
