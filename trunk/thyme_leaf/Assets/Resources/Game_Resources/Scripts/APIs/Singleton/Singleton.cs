using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// for singleton pattern
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public const string TAG = "[Singleton<T>]";
	public int funcTest = 0;
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

            //GameObject systemObject;
            //systemObject = GameObject.Find("_System");
            //if (systemObject == null)
            //{
            //    systemObject = new GameObject();
            //    systemObject.name = "_System";
            //    DontDestroyOnLoad(systemObject);
            //}

            // Applying DCL
            if (_instance == null)
            {
                lock (typeof(Singleton<T>))
                {
                    T[] objs = FindObjectsOfType<T>();
                    if (objs.Length > 1)
                    {
                        Debug.LogError(TAG + " Something went really wrong " +
                                       " - there should never be more than 1 singleton!" +
                                       " Reopenning the scene might fix it.");
                        return null;
                    }
                    else if (objs.Length == 1)
                    {
                        _instance = objs[0];
                    }
                    else
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).ToString();
                    }

                    DontDestroyOnLoad(_instance.gameObject);
                }
            }

            return _instance;
        }
    }

    /// <summary>
    /// parent this to another gameobject by string
    /// call from Awake() if you so desire.
    /// </summary>
    protected void SetParent(string parentName)
    {
        if (parentName != null)
        {
            GameObject parent = GameObject.Find(parentName);
            if (parent == null)
            {
                parent = new GameObject();
                parent.name = parentName;
            }
            this.transform.parent = parent.transform;
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

    protected virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}