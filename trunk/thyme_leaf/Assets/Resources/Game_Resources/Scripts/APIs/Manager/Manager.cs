using UnityEngine;
using System.Collections;

/// <summary>
/// All the manager classes must be derived from this class.
/// </summary>
/// <typeparam name="T">
/// The name of subclass
/// </typeparam>

public class Manager<T> : SingletonGroup<T> where T : MonoBehaviour
{
    public new const string TAG = "[Manager]";
}
