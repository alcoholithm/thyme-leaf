using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// front-end class name
/// <typeparam name="U"></typeparam>
/// group class name
/// 
public class SingletonGroup<T> : Singleton<T> where T : MonoBehaviour
{
    public new const string TAG = "[SingletonChild]";

    public new static T Instance
    {
        get
        {
            if (Singleton<T>.Instance == null)
                return _instance;
            
            string groupName = "_" + typeof(T).BaseType.ToString().Split('`')[0];
            GameObject manager = GameObject.Find(groupName);
            if (manager == null)
            {
                manager = new GameObject();
                manager.name = groupName;
                manager.transform.parent = GameObject.Find("_System").transform;
                DontDestroyOnLoad(manager);
            }

            _instance.gameObject.transform.parent = manager.transform;

            return _instance;
        }
    }
}
