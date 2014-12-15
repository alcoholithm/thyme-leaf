using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerSpawner : Singleton<TowerSpawner>
{
	public new const string TAG = "[TowerSpawner]";

    // it is single tower. if you need more towers talk to deokil. never touch this code in your mind
    public GameObject[] towers;
    public int initPoolSize = 100;
    public int maxPoolSize = 200;

    void Awake()
    {
        foreach (GameObject tower in towers)
        {
            ObjectPoolingManager.Instance.CreatePool(tower, initPoolSize, maxPoolSize, false);
        }
    }

    public Tower getTower()
    {
        return ObjectPoolingManager.Instance.GetObject(towers[0].name).GetComponent<Tower>();
    }

    public Tower getObject(string name)
    {
        return ObjectPoolingManager.Instance.GetObject(name).GetComponent<Tower>();
    }

    public Tower DynamicInstantiate()
    {
        return getTower();
    }

    public Tower Allocate()
    {
        return DynamicInstantiate();
    }

    public void Free(GameObject gameObject)
    {
        //Do you need some more handling the object?
        gameObject.SetActive(false);
    }
}