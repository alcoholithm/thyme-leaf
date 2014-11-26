using UnityEngine;
using System.Collections;

public class NullCommand : ICommand
{
    public const string TAG = "[NullCommand]";

    public void Execute()
    {
        Debug.Log(TAG + "NullCommand");
    }
}
