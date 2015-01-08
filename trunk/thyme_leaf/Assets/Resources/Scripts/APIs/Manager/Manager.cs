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
    protected override void Awake()
    {
        base.Awake();

        if (IsGlobal)
        {
            GameObject parent = SetParent("_Manager");
            DontDestroyOnLoad(parent.gameObject);
        }
        else
        {
            GameObject parent = SetParent("_LocalManager");
        }
    }

    public new const string TAG = "[Manager]";
}
