using UnityEngine;
using System.Collections;

public class EnumConverter {
    public static string getSpawnerNameBy(SpawnerType manager){
        switch (manager)
        {
            case SpawnerType.HERO_SPAWNER:
                return "HeroSpawner";
            case SpawnerType.TOWER_SPAWNER:
                return "TowerSpawner";
            case SpawnerType.TROVANT_SPAWNER:
                return "TrovantSpawner";
            default:
                return "";
        }
    }
}
