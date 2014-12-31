using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroSpawner : Singleton<HeroSpawner>
{
    public new const string TAG = "[AutomatSpawner]";

    // we hava only single automat
    // I think we have to more automats, doesn't it?

    [SerializeField]
    protected GameObject[] automats;
    [SerializeField]
    protected int initPoolSize = 100;
    [SerializeField]
    protected int maxPoolSize = 200;

    void Awake()
    {
        if (Network.isServer)
        {
            foreach (GameObject automat in automats)
            {
                ObjectPoolingManager.Instance.CreatePool(gameObject, automat, initPoolSize, maxPoolSize, false);
            }
        }
    }

    public Hero getHero()
    {
        return ObjectPoolingManager.Instance.GetObject(automats[0].name).GetComponent<Hero>();
    }

    public Hero getObject(string name)
    {
        return ObjectPoolingManager.Instance.GetObject(name).GetComponent<Hero>();
    }

    public Hero DynamicInstantiate()
    {
        return getHero();
    }

    public Hero Allocate()
    {
        return DynamicInstantiate();
    }

    public void Free(GameObject gameObject)
    {
        //Do you need some more handling the object?
        gameObject.transform.parent = transform;
        gameObject.SetActive(false);
    }

}