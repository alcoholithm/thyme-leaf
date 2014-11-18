using UnityEngine;
using System.Collections;

/// <summary>
/// All the GameEntities which want to communicate with message must implement this interface.
/// </summary>
public interface IHandler {
    void HandleMessage(Message msg);
}
