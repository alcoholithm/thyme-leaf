using UnityEngine;
using System.Collections;

/// <summary>
/// for singleton pattern
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private bool isGlobal;

    public bool IsGlobal
    {
        get { return isGlobal; }
        set { isGlobal = value; }
    }

    protected volatile static T _instance;

    /*
     * Followings are unity callback methods
     */ 
    protected virtual void Awake()
    {
        if (_instance == null)
            _instance = GetComponent<T>();

        if (isGlobal)
            DontDestroyOnLoad(_instance.gameObject);
    }

    /// <summary>
    /// parent this to another gameobject by string
    /// call from Awake() if you so desire.
    /// </summary>
    protected GameObject SetParent(string parentName)
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

            return parent;
        }

        return null;
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
        if (isGlobal)
            applicationIsQuitting = true;
    }

    /*
     * Followings are Attributes
     */
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
                }
            }

            return _instance;
        }
    }
    public const string TAG = "[Singleton]";
}