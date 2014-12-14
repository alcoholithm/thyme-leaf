using UnityEngine;
using System.Collections;

public class EnumConverter {
    public static string getManagerName(EnumManager manager){
        switch (manager)
        {
            case EnumManager.HERO_SPAWNER:
                return "HeroSpawner";
            case EnumManager.TOWER_SPAWNER:
                return "TowerSpawner";
            case EnumManager.TROVANT_SPAWNER:
                return "TrovantSpawner";
            default:
                return "";
        }
    }
}
