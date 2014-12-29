using UnityEngine;
using System.Collections;

/// <summary>
/// Class "Manager" is the root of the all managers.
/// Every manager must be derived from this class.
/// </summary>
/// <typeparam name="T">
/// The name of subclass
/// </typeparam>

public class Manager<T> : Singleton<T> where T : MonoBehaviour
{
    protected virtual void Awake()
    {
        GameObject parent = SetParent("_Manager");
        DontDestroyOnLoad(parent.gameObject);
    }

    public new const string TAG = "[Manager]";
}
