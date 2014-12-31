using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour//: Singleton<Spawner>
{
    public new const string TAG = "[Spawner]";

    [SerializeField]
    protected GameObject[] automats;
    [SerializeField]
    protected GameObject[] towers;
    [SerializeField]
    protected GameObject[] projectiles;
    [SerializeField]
    protected GameObject[] trovants;

    [SerializeField]
    protected int initPoolSize = 100;
    [SerializeField]
    protected int maxPoolSize = 200;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (Network.isServer)
        {
            foreach (GameObject automat in automats)
            {
                ObjectPoolingManager.Instance.CreatePool(gameObject, automat, initPoolSize, maxPoolSize, false);
            }
        }
    }

    public void OnNetworkLoadedLevel()
    {
        //Debug.Log("Start Object Pool");
        //foreach (GameObject automat in automats)
        //{
        //    ObjectPoolingManager.Instance.CreatePool(gameObject, automat, initPoolSize, maxPoolSize, false);
        //}
    }


    // use only test
    public Hero GetHero()
    {
        return GetHero(AutomatType.FRANSIS_TYPE1);
    }

    public Agt_Type1 GetTower()
    {
        return GetTower(TowerType.APT);
    }

    public Projectile GetProjectile()
    {
        return GetProjectile(ProjectileType.POISON);
    }

    public Hero GetTrovant()
    {
        return GetTrovant(TrovantType.COMMA);
    }

    

    public Hero GetHero(AutomatType type)
    {
        return ObjectPoolingManager.Instance.GetObject(automats[(int)type].name).GetComponent<Hero>();
    }

    public Hero GetTrovant(TrovantType type)
    {
        return ObjectPoolingManager.Instance.GetObject(trovants[(int) type].name).GetComponent<Hero>();
    }

    public Agt_Type1 GetTower(TowerType type)
    {
        return ObjectPoolingManager.Instance.GetObject(towers[(int)type].name).GetComponent<Agt_Type1>();
    }

    public Projectile GetProjectile(ProjectileType type)
    {
        return ObjectPoolingManager.Instance.GetObject(projectiles[(int) type].name).GetComponent<Projectile>();
    }


    //public Hero GetObject(string name)
    //{
    //    return ObjectPoolingManager.Instance.GetObject(name).GetComponent<Hero>();
    //}

    //public Agt_Type1 GetObject(string name)
    //{
    //    return ObjectPoolingManager.Instance.GetObject(name).GetComponent<Agt_Type1>();
    //}

    //public Hero GetObject(string name)
    //{
    //    return ObjectPoolingManager.Instance.GetObject(name).GetComponent<Hero>();
    //}


    //public Hero DynamicInstantiate()
    //{
    //    return GetHero();
    //}

    //public Hero Allocate()
    //{
    //    return DynamicInstantiate();
    //}

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

    private static Spawner instance;
    public static Spawner Instance
    {
        get { return instance; }
    }
}