using UnityEngine;
using System.Collections;

/// <summary>
/// All the manager classes must be derived from this class.
/// </summary>
/// <typeparam name="T">
/// The name of subclass
/// </typeparam>

public class Manager<T> : Singleton<T> where T : MonoBehaviour
{
    public const string TAG = "[Manager]";

    public static T Instance
    {
        get
        {
            if (Singleton<T>.Instance == null)
                return _instance;

            GameObject manager = GameObject.Find("_Manager");
            if (manager == null)
            {
                manager = new GameObject();
                manager.name = "_Manager";
                manager.transform.parent = GameObject.Find("_System").transform;
                DontDestroyOnLoad(manager);
            }

            _instance.gameObject.transform.parent = manager.transform;

            return _instance;
        }
    }
}
