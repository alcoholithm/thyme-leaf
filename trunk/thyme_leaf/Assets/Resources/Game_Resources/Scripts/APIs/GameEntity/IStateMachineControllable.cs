using UnityEngine;
using System.Collections;

public interface IStateMachineControllable<TOwner> where TOwner : GameEntity
{
    void ChangeState(State<TOwner> newState);
    void RevertToPreviousState();
}
