using UnityEngine;
using System.Collections;

/// <summary>
/// All the game entities must be derived from this class.
/// </summary>
public class GameEntity : MonoBehaviour, IHandler {
    public const string TAG = "[GameEntity]";

//    private StateMachine<GameEntity> stateMachine;

    public void HandleMessage(Message msg)
    {
        throw new System.NotImplementedException();
    }
}
