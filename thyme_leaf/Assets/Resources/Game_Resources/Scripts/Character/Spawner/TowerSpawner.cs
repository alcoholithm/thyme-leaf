using UnityEngine;
using System.Collections;

public class TowerSpawner : Spawner<Tower>
{
	public new const string TAG = "[TowerSpawner]";
    //private MemoryPool<Tower> memoryPool;

	private static volatile TowerSpawner instance;
	private static object locker = new Object();
	
	private TowerSpawner(){}
	
	public static TowerSpawner Instance
	{
		get 
		{
			if(instance == null)
			{
				lock(locker)
				{
					instance = new TowerSpawner();
				}
			}
			return instance;
		}
	}

    /*
     * followings are unity callback methods
     */ 
    void Awake()
    {
        //memoryPool = new MemoryPool<Tower>(GetComponentsInChildren<Tower>());
    }

    /*
     * followings are member functions
     */ 
//    public Tower Allocate()
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

	protected override Tower DynamicInstantiate()
    {
        GameObject go = Instantiate(transform.GetChild(0).gameObject) as GameObject;
        return go.GetComponent<Tower>();
    }

    
}