using UnityEngine;
using System.Collections;

public class TowerSpawner : Singleton<TowerSpawner>
{
    private MemoryPool<Tower> memoryPool;

    void Awake()
    {
        memoryPool = new MemoryPool<Tower>(GetComponentsInChildren<Tower>());
    }

    public Tower Allocate()
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

    public void Free()
    {
        throw new System.NotImplementedException();
    }

    private Tower DynamicInstantiate()
    {
        GameObject go = Instantiate(transform.GetChild(0).gameObject) as GameObject;
        return go.GetComponent<Tower>();
    }

    public new const string TAG = "[Spawner]";
}