using UnityEngine;
using System.Collections;

public class TrovantSpawner : MonoBehaviour {

    public const string TAG = "[TrovantSpawner]";
        
    public GameObject[] trovants;
    public int initPoolSize = 100;
    public int maxPoolSize = 200;

    void Awake()
    {
        foreach (GameObject trovant in trovants)
        {
            ObjectPoolingManager.Instance.CreatePool(trovant, initPoolSize, maxPoolSize, false);
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
        Destroy(gameObject);
    }
}
