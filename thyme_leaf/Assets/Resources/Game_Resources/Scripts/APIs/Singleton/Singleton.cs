using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// for singleton pattern
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public const string TAG = "[Singleton<T>]";

    protected volatile static T _instance;

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning(TAG + " Instance '" + typeof(T) +
                                 "' already destroyed on application quit." +
                                 " Will not create again - returning null.");
                return null;
            }

            GameObject systemObject = GameObject.Find("_System");
            if (systemObject == null)
            {
                systemObject = new GameObject();
                systemObject.name = "_System";
                DontDestroyOnLoad(systemObject);
            }

            // Applying DCL
            if (_instance == null)
            {
                lock (typeof(Singleton<T>))
                {
                    _instance = FindObjectOfType<T>();

                    if (FindObjectsOfType<T>().Length > 0)
                    {
                        Debug.LogError(TAG + " Something went really wrong " +
                                       " - there should never be more than 1 singleton!" +
                                       " Reopenning the scene might fix it.");
                        return _instance;
                    }
                }

                GameObject singleton = new GameObject();
                singleton.transform.parent = systemObject.transform;
                singleton.name = typeof(T).ToString();
                DontDestroyOnLoad(singleton);

                _instance = singleton.AddComponent<T>();
            }

            return _instance;
        }
    }

    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    private static bool applicationIsQuitting = false;

    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}