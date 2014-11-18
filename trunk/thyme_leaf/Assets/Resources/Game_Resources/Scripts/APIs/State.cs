using UnityEngine;
using System.Collections;

/// <summary>
/// All the state classes must be derived from this class
/// </summary>
/// <typeparam name="T"></typeparam>
/// 
public abstract class State<T> : MonoBehaviour {
    public const string TAG = "[State]";

    private T owner;
    //private State<T> successor;

    /// <summary>
    /// This will be called when starting the state.
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// This will be called once per frame
    /// </summary>
    public abstract void Execute();

    /// <summary>
    /// This will be called when leaving the state
    /// </summary>
    public abstract void Exit();

    //public bool OnMessage(Message msg)
    //{
    //    return false;
    //}
}
