using UnityEngine;
using System.Collections;

public class ProjectileSpawnerScript : Singleton<ProjectileSpawnerScript>
{

    public Projectile GetProjectile()
    {
        return GetProjectile(ProjectileType.POISON);
    }

    public Projectile GetProjectile(ProjectileType type)
    {
        return Spawner.Instance.GetProjectile(type);
    }


    
}
