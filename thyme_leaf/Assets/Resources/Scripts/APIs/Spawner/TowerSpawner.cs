using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerSpawner : Singleton<TowerSpawner>
{
	public new const string TAG = "[TowerSpawner]";

    // it is single tower. if you need more towers talk to deokil. never touch this code in your mind
    [SerializeField]
    private GameObject[] towers;
    [SerializeField]
    private int initPoolSize = 100;
    [SerializeField]
    private int maxPoolSize = 200;

    //void Awake()
    //{
    //    if (Network.isServer)
    //    {
    //        foreach (GameObject tower in towers)
    //        {
    //            ObjectPoolingManager.Instance.CreatePool(gameObject, tower, initPoolSize, maxPoolSize, false);
    //        }
    //    }
    //}

    public void OnNetworkLoadedLevel()
    {
        foreach (GameObject automat in towers)
        {
            ObjectPoolingManager.Instance.CreatePool(gameObject, automat, initPoolSize, maxPoolSize, false);
        }
    }

    public Agt_Type1 getTower()
    {
        return ObjectPoolingManager.Instance.GetObject(towers[0].name).GetComponent<Agt_Type1>();
    }

    public Agt_Type1 getObject(string name)
    {
        return ObjectPoolingManager.Instance.GetObject(name).GetComponent<Agt_Type1>();
    }

    public Agt_Type1 DynamicInstantiate()
    {
        return getTower();
    }

    public Agt_Type1 Allocate()
    {
        return (Instantiate(towers[0]) as GameObject).GetComponent<Agt_Type1>();
        //return DynamicInstantiate();
    }

    public void Free(GameObject gameObject)
    {
        //Do you need some more handling the object?
        gameObject.transform.parent = transform;
        gameObject.SetActive(false);
    }
}