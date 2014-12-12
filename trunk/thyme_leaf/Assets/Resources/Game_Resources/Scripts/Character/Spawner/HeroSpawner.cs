using UnityEngine;
using System.Collections;

public class HeroSpawner : Singleton<HeroSpawner>
{
    //private MemoryPool<Hero> memoryPool;



    void Awake()
    {
        //memoryPool = new MemoryPool<Hero>(GetComponentsInChildren<Hero>());
    }

    public Hero Allocate()
    {
        //Tower gameEntity = null; 
        //try
        //{
        //    gameEntity = memoryPool.Allocate();
        //}
        //catch (System.Exception e)
        //{
        //    Debug.LogException(e);
        //    gameEntity = DynamicInstantiate();
        //}
        //return gameEntity;

        return DynamicInstantiate();
    }

    public void Free(GameObject some)
    {
		Destroy(some);
        //throw new System.NotImplementedException();
    }

    private Hero DynamicInstantiate()
    {
        GameObject go;
        if (!Network.isServer && !Network.isClient)
        {
            go = Instantiate(transform.GetChild(funcTest).gameObject) as GameObject;
        }
        else
        {
            //Network.Instantiate(prefab, transform.position, transform.rotation, 0);
            go = Network.Instantiate(transform.GetChild(funcTest).gameObject, transform.position, transform.rotation,0) as GameObject;
        }
        return go.GetComponent<Hero>();
    }

    public new const string TAG = "[HeroSpawner]";
}