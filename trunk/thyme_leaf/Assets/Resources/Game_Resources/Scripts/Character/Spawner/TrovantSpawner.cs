using UnityEngine;
using System.Collections;

public class TrovantSpawner : Singleton<TrovantSpawner>{

    public new const string TAG = "[TrovantSpawner]";

    [SerializeField]
    protected GameObject[] trovants;
    [SerializeField]
    protected int initPoolSize = 100;
    [SerializeField]
    protected int maxPoolSize = 200;

    void Awake()
    {
        //if (!Network.isServer) Destroy(this);
        foreach (GameObject trovant in trovants)
        {            
            ObjectPoolingManager.Instance.CreatePool(gameObject, trovant, initPoolSize, maxPoolSize, false);
        }
    }
    
    // why do I have to return hero type when calling trovant??
    public Hero getTrovant()
    {
        return ObjectPoolingManager.Instance.GetObject(trovants[0].name).GetComponent<Hero>();
    }

    public Hero getObject(string name)
    {
        return ObjectPoolingManager.Instance.GetObject(name).GetComponent<Hero>();
    }

    public Hero DynamicInstantiate()
    {
        return getTrovant();
    }

    public Hero Allocate()
    {
        return DynamicInstantiate();
    }

    public void Free(GameObject gameObject)
    {
        gameObject.transform.parent = transform;
        gameObject.SetActive(false);        
    }
}
