using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : Singleton<Spawner>
{
    public new const string TAG = "[Spawner]";

    [SerializeField]
    private GameObject[] automats;
    [SerializeField]
    private GameObject[] towers;
    [SerializeField]
    private GameObject[] projectiles;
    [SerializeField]
    private GameObject[] trovants;

    [SerializeField]
    private int initPoolSize = 100;
    [SerializeField]
    private int maxPoolSize = 200;

    private GameObject automatPool;
    private GameObject trovantPool;
    private GameObject automatBuildingPool;
    private GameObject trovantBuildingPool;


    /**********************************/
    
    public void OnNetworkLoadedLevel()
    {
        //Debug.Log("Start Object Pool");
        //foreach (GameObject automat in automats)
        //{
        //    ObjectPoolingManager.Instance.CreatePool(gameObject, automat, initPoolSize, maxPoolSize, false);
        //}
    }

    /**********************************/

    void Start()
    {
        if (automatPool == null) automatPool = GameObject.Find("AutomatPool");
        if (trovantPool == null) trovantPool = GameObject.Find("TrovantPool");
        if (automatBuildingPool == null) automatBuildingPool = GameObject.Find("AutomatBuildingPool");
        if (trovantBuildingPool == null) trovantBuildingPool = GameObject.Find("TrovantBuildingPool");


        if (Network.isServer || Network.peerType == NetworkPeerType.Disconnected)
        {
            if(automats != null)
            foreach (GameObject go in automats)
            {
                ObjectPoolingManager.Instance.CreatePool(automatPool, go, initPoolSize, maxPoolSize, false);
            }

            if (towers != null)
            foreach (GameObject go in towers)
            {
                ObjectPoolingManager.Instance.CreatePool(automatBuildingPool, go, initPoolSize, maxPoolSize, false);
            }

            if (projectiles != null)
            foreach (GameObject go in projectiles)
            {
                ObjectPoolingManager.Instance.CreatePool(automatBuildingPool, go, initPoolSize, maxPoolSize, false);
            }

            if (trovants != null)
            foreach (GameObject go in trovants)
            {
                ObjectPoolingManager.Instance.CreatePool(trovantPool, go, initPoolSize, maxPoolSize, false);
            }
        }
    }

    /**********************************/

    public Hero GetHero(AutomatType type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(automats[(int)type].name);
        InitHero(ref go);
        return go.GetComponent<Hero>();
    }

    public Hero GetTrovant(TrovantType type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(trovants[(int)type].name);
        InitTrovant(ref go);
        return go.GetComponent<Hero>();
    }

    public Agt_Type1 GetTower(TowerType type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(towers[(int)type].name);
        InitTower(ref go);
        return go.GetComponent<Agt_Type1>();
    }

    public Projectile GetProjectile(ProjectileType type)
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject(projectiles[(int) type].name);
        InitProjectile(ref go);
        return go.GetComponent<Projectile>();
    }


    /**********************************/

    public void PerfectFree(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public void Free(GameObject gameObject)
    {
        //Do you need some more handling the object?
        gameObject.transform.parent = transform;
        gameObject.SetActive(false);
    }

    /**********************************/
    // Initialize Object

    private void InitHero(ref GameObject go)
    {
        go.layer = (int)Layer.Automart;
    }

    private void InitTrovant(ref GameObject go)
    {
        go.layer = (int)Layer.Trovant;
    }

    private void InitTower(ref GameObject go)
    {
        go.layer = (int)Layer.Tower;
    }

    private void InitProjectile(ref GameObject go)
    {
        go.layer = (int)Layer.Tower;
    }
}