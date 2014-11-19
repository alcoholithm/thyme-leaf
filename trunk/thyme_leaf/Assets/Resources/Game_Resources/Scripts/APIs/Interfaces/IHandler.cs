using UnityEngine;
using System.Collections;

/// <summary>
/// All the GameEntities which want to communicate with message must implements this interface.
/// </summary>
public interface IHandler
{
    IHandler Successor
    {
        get;
    }

    bool IsHandleable();
    void OnMessage(Message msg);
    void HandleMessage(Message msg);
}
