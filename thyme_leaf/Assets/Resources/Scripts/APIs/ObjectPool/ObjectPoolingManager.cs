using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*Singleton*/
public class ObjectPoolingManager
{
    private static volatile ObjectPoolingManager instance;
    private Dictionary<String, ObjectPool> objectPools;
    private static object syncRoot = new System.Object();

    private ObjectPoolingManager()
    {
        this.objectPools = new Dictionary<String, ObjectPool>();
    }

    public static ObjectPoolingManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new ObjectPoolingManager();
                    }
                }
            }
            return instance;
        }
    }

    public bool CreatePool(GameObject spawner, GameObject objToPool, UnitType unitType, int type, int initialPoolSize, int maxPoolSize, bool shouldShrink)
    {
        if (ObjectPoolingManager.Instance.objectPools.ContainsKey(objToPool.name))
        {
            return false;
        }
        else
        {
            ObjectPool nPool = new ObjectPool(spawner, objToPool, initialPoolSize, maxPoolSize, shouldShrink);
            ObjectPoolingManager.Instance.objectPools.Add(objToPool.name, nPool);
            return true;
        }
    }

    public GameObject GetObject(string objName)
    {
        return ObjectPoolingManager.Instance.objectPools[objName].GetObject();
    }
}