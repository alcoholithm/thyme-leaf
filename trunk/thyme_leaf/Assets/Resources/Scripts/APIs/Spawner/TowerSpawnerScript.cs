using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerSpawnerScript : Singleton<TowerSpawnerScript>
{
    public Agt_Type1 GetTower()
    {
        return GetTower(TowerType.APT);
    }

    public Agt_Type1 GetTower(TowerType type)
    {
        return Spawner.Instance.GetTower(type);
    }
}