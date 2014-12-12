using UnityEngine;
using System.Collections;

public sealed class AutomatSpawner : Spawner<Hero>
{
	public new const string TAG = "[AutomatSpawner]";
    //private MemoryPool<Hero> memoryPool;

	private static volatile AutomatSpawner instance;
	private static object locker = new Object();

	private AutomatSpawner(){}

	public static AutomatSpawner Instance
	{
		get 
		{
			if(instance == null)
			{
				lock(locker)
				{
					instance = new AutomatSpawner();
				}
			}
			return instance;
		}
	}

	void Awake()
    {
        //memoryPool = new MemoryPool<Hero>(GetComponentsInChildren<Hero>());
    }

//    public Hero Allocate()
//    {
//        //Tower gameEntity = null; 
//        //try
//        //{
//        //    gameEntity = memoryPool.Allocate();
//        //}
//        //catch (System.Exception e)
//        //{
//        //    Debug.LogException(e);
//        //    gameEntity = DynamicInstantiate();
//        //}
//        //return gameEntity;
//
//        return DynamicInstantiate();
//    }

	protected override Hero DynamicInstantiate()
    {
        GameObject go;
        if (!Network.isServer && !Network.isClient)
        {
            go = Instantiate(transform.GetChild(0).gameObject) as GameObject;
        }
        else
        {
            Debug.Log("N_Instantiate");
            //GameObject trans = Instantiate(transform.GetChild(funcTest).gameObject);
            Transform trans = transform.GetChild(0);

            //NetworkViewID viewID = Network.AllocateViewID();
            GameObject obj = Network.Instantiate(trans, transform.position, transform.rotation, 0) as GameObject;
            return obj.GetComponent<Hero>();
        }

        return go.GetComponent<Hero>();
    }

    
}