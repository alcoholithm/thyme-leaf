using UnityEngine;
using System.Collections;

public class ProjectileSpawner : Singleton<ProjectileSpawner>
{
    [SerializeField]
    GameObject[] projectiles;
    [SerializeField]
    int initPoolSize = 100;
    [SerializeField]
    int maxPoolSize = 200;

    void Awake()
    {
        foreach (GameObject tower in projectiles)
        {
            ObjectPoolingManager.Instance.CreatePool(tower, initPoolSize, maxPoolSize, false);
        }
    }

    public Projectile getProjectile()
    {
        return ObjectPoolingManager.Instance.GetObject(projectiles[0].name).GetComponent<Projectile>();
    }

    public Projectile getObject(string name)
    {
        return ObjectPoolingManager.Instance.GetObject(name).GetComponent<Projectile>();
    }

    public Projectile DynamicInstantiate()
    {
        return getProjectile();
    }

    public Projectile Allocate()
    {
        return DynamicInstantiate();
    }

    public Projectile Allocate(Transform parent)
    {
        Projectile projectile = DynamicInstantiate();
        projectile.transform.parent = parent;
        projectile.transform.position = parent.transform.position;
        projectile.transform.localScale = Vector3.one;

        return projectile;
    }

    public void Free(GameObject gameObject)
    {
        //Do you need some more handling the object?
        gameObject.SetActive(false);
    }

    public new const string TAG = "[ProjectileSpawner]";
}
