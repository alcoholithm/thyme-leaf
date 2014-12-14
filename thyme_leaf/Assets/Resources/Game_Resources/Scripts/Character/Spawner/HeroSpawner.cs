using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroSpawner : MonoBehaviour
{
	public const string TAG = "[AutomatSpawner]";

    // we hava only single automat
    // I think we have to more automats, doesn't it?
    //

    public GameObject[] automats;
    public int initPoolSize = 100;
    public int maxPoolSize = 200;

    void Awake()
    {
        foreach (GameObject automat in automats)
        {
            ObjectPoolingManager.Instance.CreatePool(automat, initPoolSize, maxPoolSize, false);
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
        gameObject.SetActive(false);           
    }
}