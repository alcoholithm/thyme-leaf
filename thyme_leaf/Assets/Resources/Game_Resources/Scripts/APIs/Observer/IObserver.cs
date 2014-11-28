using UnityEngine;
using System.Collections;

/// <summary>
/// for observer pattern
/// </summary>
public interface IObserver<T> {
    void Refresh<T>();
}
