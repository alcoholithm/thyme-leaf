using UnityEngine;
using System.Collections;

/// <summary>
/// All the game entities must be derived from this class.
/// </summary>
public abstract class GameEntity : MonoBehaviour, IHandler {
    public const string TAG = "[GameEntity]";

//    private StateMachine<GameEntity> stateMachine;

    public abstract void HandleMessage(Message msg);
}
